using Godot;
using System;

namespace stellarthing;

public partial class Player : CharacterBody3D {
	[Export]
	public int Speed { get; set; } = 50;
	[Export]
	public double RunningThingy { get; set; } = 1.25;
	[Export]
	public Node3D Model { get; set; }
	[Export]
	public Camera3D Camêra { get; set; }
	[Export]
	public Node3D ThingFafferyFuckery { get; set; }

	public static Camera3D Camera { get; private set; }
	public static Vector3 ThingFafferyFuckeryThingy { get; private set; }
	public static Vector3 ThingFafferyFuckeryThingyHehehehe { get; private set; }
	AnimationPlayer modelAnimator;

    public override void _Ready()
    {
		Camera = Camêra;
        modelAnimator = Model.GetNode<AnimationPlayer>("animation_player");
    }

    public override void _PhysicsProcess(double delta)
	{
		// pausing :D
		if (Input.IsActionJustPressed("pause")) {
			GetNode<Control>("/root/hud/pause/pause").Visible = true;
			GetTree().Paused = true;
		}

		// movement
		float run = Input.IsActionPressed("run") ? (float)RunningThingy : 1.0f;
		
		Vector3 dir = Vector3.Zero;
		if (Input.IsActionPressed("move_left")) dir.X -= 1;
		if (Input.IsActionPressed("move_right")) dir.X += 1;
		if (Input.IsActionPressed("move_up")) dir.Z -= 1;
		if (Input.IsActionPressed("move_down")) dir.Z += 1;

		dir = dir.Normalized();
        Velocity = dir * Speed * new Vector3(run, 0, run);
		MoveAndSlide();

		if (!dir.IsZeroApprox()) {
			Model.Basis = Basis.LookingAt(dir);
		}

		// animate
		if (!dir.IsZeroApprox() && run < 1.1f) {
			modelAnimator.Play("walk");
		}
		else if (!dir.IsZeroApprox() && run > 1.1f) {
			modelAnimator.Play("walk", customSpeed: (float)RunningThingy * 1.3f);
		}
		else {
			modelAnimator.Play("idle");
		}

		ThingFafferyFuckeryThingy = ThingFafferyFuckery.GlobalPosition;
		ThingFafferyFuckeryThingyHehehehe = ThingFafferyFuckery.GlobalRotation;
	}
}
