using Godot;
using System;


public partial class Player : CharacterBody2D
{
[Signal]
public delegate void ModifyHPEventHandler(float value);
private float _HP;
     public float HP{
        get{
            return _HP;
        }
        set{ 
            
           EmitSignal(SignalName.ModifyHP, (float)value);
           GD.Print(HP);
        }
    }
	[Export] public float TopSpeed = 0;
	[Export] public float Acceleration = 0.1f;
	[Export] public float Braking = 0.6f;
	[Export] public float Gravity = 20;
	[Export] public float JumpPower = 600;
    [Export] public float Damage = 10;

    void CalculateHp(float value){
        _HP = value;
    }
    

}
