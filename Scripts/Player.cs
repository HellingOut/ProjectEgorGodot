using System;
using Godot;

public partial class Player : CharacterBody2D
{
	float _direction = 0;
	public float Speed = 0;
	[Export]
	public float TopSpeed = 300;
	[Export]
	public float Acceleration = 0.1f;
	[Export]
	public float Braking = 0.6f;
	[Export]
	public float Gravity = 20;
	[Export]
	public float JumpPower = 600;
	void Move(ref Vector2 localVelocity, double delta){
		_direction = Input.GetAxis("walk_left", "walk_right");
		if(_direction != 0){
			localVelocity.X = Mathf.Lerp(localVelocity.X, TopSpeed * _direction, Acceleration);
		}
		else if(_direction == 0){
			localVelocity.X = Mathf.Lerp(localVelocity.X, 0, Braking);
		}
	}
	void Jump(ref Vector2 localVelocity){
		if (IsOnFloor()){ 
			if(Input.IsActionJustPressed("jump")){
				localVelocity.Y -= JumpPower;
			}
		}		
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 localVelocity = Velocity;
		Move(ref localVelocity,delta);
		Velocity = localVelocity;
		Jump(ref localVelocity);
		Velocity = localVelocity;
		localVelocity.Y += Gravity;
		Velocity = localVelocity;
		Speed = Velocity.X;
		MoveAndSlide();
		
	}
}
