using Godot;
using System;

[GlobalClass]
public partial class Enemy : CharacterBody2D
{
	CollisionShape2D _collision = null;
	float _direction = 0;
	[Export] public float HP = 100;
	[Export] public float TopSpeed = 0;
	[Export] public float Acceleration = 0.1f;
	[Export] public float Braking = 0.6f;
	[Export] public float Gravity = 20;
	[Export] public float JumpPower = 600;
	[Export] public float Damage = 10;

	public override void _Ready()
	{
		
	}

	// void YAnimationsPlay(Vector2 vel)
	// {
	// 	if (vel.Y < 0)
	// 	{
			
	// 	}
	// 	else
	// 	{
			
	// 	}
	// }

	void GravityApply(ref Vector2 localVelocity, double delta)
	{
		localVelocity.Y += Gravity * (float)delta;
	}

	void Move(ref Vector2 localVelocity, double delta)
	{
		localVelocity.X = Mathf.Lerp(localVelocity.X,0, Braking * (float)delta);
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
			// YAnimationsPlay(localVelocity);
			GravityApply(ref localVelocity, delta);
		}
		// else
		// {
		// 	// Jump(ref localVelocity);
		// 	if (!FallThrough())
		// 		RecoverMask();
		// }
		Velocity = localVelocity;
		MoveAndSlide();
	}
}
