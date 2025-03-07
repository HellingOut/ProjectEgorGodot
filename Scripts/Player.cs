using System;
using Godot;

public partial class Player : CharacterBody2D
{
	float _direction = 0;

	[Export]
	public float Speed = 200;
	[Export]
	public float Gravity = 100;
	[Export]
	public float JumpPower = 400;
	bool IsJumpedAnimationEnded(){
		if(Velocity.Y == JumpPower)
			return true;
		else if(IsOnWall())
			return true;
		else if(IsOnFloor())
			return true;
		//else if(collision)
		return false;
	}
	void Move(ref Vector2 localVelocity, double delta){
		
		_direction = Input.GetAxis("walk_left", "walk_right");
		if(_direction != 0){
			localVelocity.X += _direction * Speed;
		}
		else if(_direction == 0) {
			localVelocity.X = Mathf.Lerp(localVelocity.X, 0,0.05f);
		}
	}
	void Jump(ref Vector2 localVelocity, double delta){
		bool jumping = false;
		if (IsOnFloor())
		{ 
			if(Input.IsActionJustPressed("jump"))
			{
				localVelocity.Y = -JumpPower;
				jumping = true;
			}
		}
		else if(IsJumpedAnimationEnded() || !jumping){
				localVelocity.Y = Mathf.Lerp(localVelocity.Y, Gravity, 0.5f );
			// Логика чтоб кирпичем при прыжке сразу не падал
		}
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 localVelocity = Velocity;
		Move(ref localVelocity,delta);
		Velocity = localVelocity;
		Jump(ref localVelocity,delta);
		Velocity = localVelocity;
		MoveAndSlide();
		
	}
}
