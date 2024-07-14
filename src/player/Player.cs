using Godot;
using System;

namespace spacegame;

public partial class Player : CharacterBody2D {
	[Export]
	public int Speed { get; set; } = 400;
	[Export]
	public double RunningThingy { get; set; } = 1.25;

	public override void _PhysicsProcess(double delta)
	{
		LookAt(GetGlobalMousePosition());
		double run = Input.IsActionPressed("run") ? RunningThingy : 1.0;
		Velocity = Transform.X * Input.GetAxis("move_backwards", "move_forwards") * Speed *
			new Vector2((float)run, (float)run);
		MoveAndSlide();
	}
}
