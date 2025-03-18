using Godot;
using System;

public partial class UI : Label
{
	Node2D _player = null;
	public override void _Ready()
	{
		_player = GetNode<Node2D>("../../..");
		if(_player != null){
			GD.Print("1");
		}
		else
		{
			GD.Print("2");
		}
	}
}
