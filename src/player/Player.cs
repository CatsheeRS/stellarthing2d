using Godot;
using System;

namespace stellarthing;

// shout out to https://github.com/WaffleAWT/Godot-4.1-Third-Person-Controller
// the movement is based on that
public partial class Player : CharacterBody3D {
	[Export]
	public double WalkSpeed { get; set; } = 5.0;
	[Export]
	public double RunMultiplier { get; set; } = 1.25;
	[Export]
	public double JumpStrength { get; set; } = 5.0;
	[Export]
	public Node3D Model { get; set; }
	[Export]
	public Camera3D Camêra { get; set; }
	[Export]
	public Node3D ThingFafferyFuckery { get; set; }
	[Export]
	public RayCast3D RaycastThing { get; set; }
	double Speed = 0;
	Vector3 SnapVector = Vector3.Zero;

	public static Camera3D Camera { get; private set; }
	public static Vector3 ThingFafferyFuckeryThingy { get; private set; }
	public static Vector3 ThingFafferyFuckeryThingyHehehehe { get; private set; }
	public static bool CommenceOnslaughter { get; set; }
	AnimationPlayer modelAnimator;
	public static Vector3 OffsetThingy = new(0, -1.5f, -2);
	double gravity = (double)ProjectSettings.GetSetting("physics/3d/default_gravity");
	Label instruction;

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

		var dir = Vector3.Zero;
		dir.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		dir.Z = Input.GetActionStrength("move_backwards") - Input.GetActionStrength("move_forwards");
		dir = dir.Rotated(Vector3.Up, Camêra.Rotation.Y);

		if (Input.IsActionJustPressed("run")) Speed = WalkSpeed * RunMultiplier;
		else Speed = WalkSpeed;

		Velocity = new Vector3(
            (float)(dir.X * Speed),
			(float)(Velocity.Y - gravity * delta),
            (float)(dir.Z * Speed)
        );

		if (dir >= Vector3.One) {
			Model.Rotation = new Vector3(0, (float)Mathf.LerpAngle(Model.Rotation.Y,
				Mathf.Atan2(Velocity.X, Velocity.Z), 0.15), 0);
		}

		bool justLanded = IsOnFloor() && SnapVector == Vector3.Zero;
		bool isJumping = IsOnFloor() && Input.IsActionJustPressed("jump");
		if (isJumping) {
			Velocity = new Vector3(Velocity.X, (float)JumpStrength, Velocity.Z);
			SnapVector = Vector3.Zero;
		}
		else if (justLanded) {
			SnapVector = Vector3.Down;
		}

		ApplyFloorSnap();
		MoveAndSlide();

		// animate
		if (!dir.IsZeroApprox() && !Input.IsActionJustPressed("run")) {
			modelAnimator.Play("walk");
		}
		else if (!dir.IsZeroApprox() && Input.IsActionJustPressed("run")) {
			modelAnimator.Play("walk", customSpeed: (float)RunMultiplier * 1.3f);
		}
		else {
			modelAnimator.Play("idle");
		}

		// funni editor stuff
		ThingFafferyFuckery.Position = OffsetThingy;
		ThingFafferyFuckeryThingy = ThingFafferyFuckery.GlobalPosition;
		ThingFafferyFuckeryThingyHehehehe = Model.Rotation;

		// // thy onslaughter (removing items)
		// instruction ??= GetNode<Label>("/root/hud/instruction_notcraft");
		
		// instruction.Visible = CommenceOnslaughter;
		// if (CommenceOnslaughter) {
		// 	if (Input.IsMouseButtonPressed(MouseButton.Left)) {
		// 		Onslaughter.OnslaughterShallCommence(RaycastThing.GetCollider());
		// 	}

		// 	if (Input.IsMouseButtonPressed(MouseButton.Right)) {
		// 		CommenceOnslaughter = false;
		// 	}
		// }
	}
}
