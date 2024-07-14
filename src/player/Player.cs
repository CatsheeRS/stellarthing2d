using Godot;
using System;

namespace spacegame;

public partial class Player : CharacterBody2D {
	[Export]
	public int Speed { get; set; } = 400;
	[Export]
	public double RunningThingy { get; set; } = 1.25;
	[Export]
	public Sprite2D Preview { get; set; }
	[Export]
	public RayCast2D PointingRaycast { get; set; }
	readonly PackedScene lol = GD.Load<PackedScene>("res://blocks/test_block1.tscn");

	public override void _PhysicsProcess(double delta)
	{
		// movement
		LookAt(GetGlobalMousePosition());
		double run = Input.IsActionPressed("run") ? RunningThingy : 1.0;
		Velocity = Transform.X * Input.GetAxis("move_backwards", "move_forwards") * Speed *
			new Vector2((float)run, (float)run);
		MoveAndSlide();

		// placing stuff
		if (Input.IsActionJustPressed("left_click")) {
			var m = lol.Instantiate<Block>();
			m.Position = Preview.GlobalPosition;
			m.Rotation = Rotation;
			GetParent().AddChild(m);
		}

		// ffuts gnicalp
		if (Input.IsActionJustPressed("right_click")) {
			var xd = PointingRaycast.GetCollider();
			if (xd != null) {
				// if we checked for physicsbodies we would be able to destroy people
				if (xd is Block m) {
					m.QueueFree();
				}
			}
		}
	}
}
