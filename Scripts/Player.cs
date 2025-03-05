using Godot;

public partial class Player : CharacterBody2D
{
	float _direction = 0;

	[Export]
	public float Speed = 200;
	[Export]
	public float Gravity = 100;
	[Export]
	public float JumpPower = 40;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		_direction = Input.GetAxis("walk_left", "walk_right");

		if (_direction != 0)
			velocity.X = _direction * Speed;
		else
			velocity.X = 0;
		
		if (IsOnFloor())
		{ 
			if(Input.IsActionJustPressed("jump"))
			{
				velocity.Y = -JumpPower;
			}
		}
		else
		{
			velocity.Y += (float)(Gravity * delta);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
