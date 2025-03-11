using System;
using System.Text;
using Godot;

public partial class Player : CharacterBody2D
{
	AnimatedSprite2D sprite = null;
	CollisionShape2D collision = null;
	float _direction = 0;
	[Export] public float TopSpeed = 300;
	[Export] public float Acceleration = 0.1f;
	[Export] public float Braking = 0.6f;
	[Export] public float Gravity = 20;
	[Export] public float JumpPower = 600;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
	}

	void Move(ref Vector2 localVelocity, double delta){
		_direction = Input.GetAxis("walk_left", "walk_right");

		if(_direction != 0){
			localVelocity.X = Mathf.Lerp(localVelocity.X, TopSpeed * _direction, Acceleration * (float)delta);
			sprite.Scale = new Vector2(_direction, 1);
			sprite.Play("walk");
		}
		else if(_direction == 0){
			sprite.Play("idle");
			localVelocity.X = Mathf.Lerp(localVelocity.X, 0, Braking * (float)delta);
		}
	}
	void Jump(ref Vector2 localVelocity){
		if (IsOnFloor()){ 
			if(Input.IsActionJustPressed("jump")){
				localVelocity.Y = -JumpPower;
			}
		}
		
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 localVelocity = Velocity;

		Move(ref localVelocity, delta);
		Jump(ref localVelocity);
		
		if (!IsOnFloor()){
			localVelocity.Y += Gravity * (float)delta;
			if(localVelocity.Y < 0)
			{
				sprite.Play("jump");
			}
			else{
				sprite.Play("fall");
			}
		}
		
		Velocity = localVelocity;
		MoveAndSlide();
		
	}
}
