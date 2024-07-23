using Godot;
using System;

namespace stellarthing;

public partial class PlayerPlayerPlayer : CharacterBody2D {
	[Export]
	public int Speed { get; set; } = 400;
	[Export]
	public double RunningThingy { get; set; } = 1.25;
	[Export]
	public AnimatedSprite2D Sprite { get; set; }
	[Export]
	public Sprite2D Preview { get; set; }
	[Export]
	public RayCast2D PointingRaycast { get; set; }
	[Export]
	public Control PauseThingLmao { get; set; }

	readonly PackedScene lol = GD.Load<PackedScene>("res://blocks/test_block1.tscn");
	// true = right, false = left
	// not talking about politics
	bool lastSide = true;

    public override void _PhysicsProcess(double delta)
	{
		// pausing :D
		if (Input.IsActionJustPressed("pause")) {
			PauseThingLmao.Visible = true;
			GetTree().Paused = true;
		}

		// movement
		float run = Input.IsActionPressed("run") ? (float)RunningThingy : 1.0f;
		Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = dir * Speed * new Vector2(run, run);
		MoveAndSlide();

		// animations
		// first figure if it's idle or walk or run
		string anim = "idle_";
		if (!dir.IsZeroApprox()) {
			if (run < 1.1) anim = "walk_";
			else anim = "run_";
		}

		// then figure out the direction
		if (Input.IsActionPressed("move_right")) Sprite.FlipH = false;
		if (Input.IsActionPressed("move_left")) Sprite.FlipH = true;

		if (Input.IsActionPressed("move_up")) anim += "up";
		else anim += "side";

		// actually animate :D
		// we do the check so it doesn't constantly reset
		if (Sprite.Animation != anim) Sprite.Animation = anim;

		// // placing stuff
		// if (Input.IsActionJustPressed("left_click")) {
		// 	var m = lol.Instantiate<Block>();
		// 	m.Position = Preview.GlobalPosition;
		// 	m.Rotation = Rotation;
		// 	GetParent().AddChild(m);
		// }

		// // ffuts gnicalp
		// if (Input.IsActionJustPressed("right_click")) {
		// 	var xd = PointingRaycast.GetCollider();
		// 	if (xd != null) {
		// 		// if we checked for physicsbodies we would be able to destroy people
		// 		if (xd is Block m) {
		// 			m.QueueFree();
		// 		}
		// 	}
		// }
	}
}
