using System;
using System.Reflection;
using System.Text;
using Godot;

public partial class Player : CharacterBody2D
{
	AnimatedSprite2D _sprite = null;
	CollisionShape2D _collision = null;
	Area2D _area2D = null;
	float _direction = 0;
	[Export] public float TopSpeed = 300;
	[Export] public float Acceleration = 0.1f;
	[Export] public float Braking = 0.6f;
	[Export] public float Gravity = 20;
	[Export] public float JumpPower = 600;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_area2D = GetNode<Area2D>("Area2D");
		_area2D.Connect("body_entered", new Godot.Callable(this,"OnBodyEntered"));
	}

	void OnBodyEntered(Node2D body){
		var bodyVelocityObject = body.Get("velocity");
		var bodyVelocity = bodyVelocityObject.AsVector2();
		GD.Print(bodyVelocity.X);
		GD.Print(Velocity.X);
		if(body.IsInGroup("Enemy") && (Mathf.Abs(Velocity.X) > Mathf.Abs(bodyVelocity.X))){
			bodyVelocity.X = 100 * Mathf.Sign(Velocity.X);
		}
			body.Set("velocity",bodyVelocity);
		}

	void YAnimationsPlay(Vector2 vel)
	{
		if (vel.Y < 0)
		{
			_sprite.Play("jump");
		}
		else
		{
			_sprite.Play("fall");
		}
	}

	void GravityApply(ref Vector2 localVelocity, double delta)
	{
		localVelocity.Y += Gravity * (float)delta;
	}

	void Move(ref Vector2 localVelocity, double delta)
	{
		_direction = Input.GetAxis("walk_left", "walk_right");

		if (_direction != 0)
		{
			localVelocity.X = Mathf.Lerp(localVelocity.X, TopSpeed * _direction, Acceleration * (float)delta);
			_sprite.Scale = new Vector2(_direction, 1);
			_sprite.Play("walk");
		}
		else if (_direction == 0)
		{
			_sprite.Play("idle");
			localVelocity.X = Mathf.Lerp(localVelocity.X, 0, Braking * (float)delta);
		}
	}
	void Jump(ref Vector2 localVelocity)
	{
		if (Input.IsActionPressed("jump"))
		{
			localVelocity.Y = -JumpPower;
		}
	}

	bool FallThrough()
	{
		if (Input.IsActionPressed("fall_through"))
		{
			SetCollisionMaskValue(2, false);
			return true;
		}
		return false;
	}

	void RecoverMask()
	{
		SetCollisionMaskValue(2, true);
	}
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 localVelocity = Velocity;
		Move(ref localVelocity, delta);

		if (!IsOnFloor())
		{
			YAnimationsPlay(localVelocity);
			GravityApply(ref localVelocity, delta);
		}
		else
		{
			Jump(ref localVelocity);
			if (!FallThrough())
				RecoverMask();
		}
		Velocity = localVelocity;
		MoveAndSlide();


	}

}
