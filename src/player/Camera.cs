using Godot;
using System;

namespace stellarthing;

public partial class Camera : Node3D {
	[Export]
	public double WalkFov { get; set; } = 75.0;
	[Export]
	public double RunFov { get; set; } = 90.0;
	[Export]
	public SpringArm3D SpringArm { get; set; }
	[Export]
	public Camera3D Kammera { get; set; }

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eVent) {
			RotateY(-eVent.Relative.X * 0.005f);
			SpringArm.RotateX(eVent.Relative.Y * 0.005f);
			SpringArm.Rotation = new Vector3(Mathf.Clamp(SpringArm.Rotation.X, (-Mathf.Pi / 4) - 180, Mathf.Pi / 4),
				SpringArm.Rotation.Y, SpringArm.Rotation.Z);
		}
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("run")) {
			Kammera.Fov = (float)Mathf.Lerp(Kammera.Fov, RunFov, 0.05);
        }
		else {
			Kammera.Fov = (float)Mathf.Lerp(Kammera.Fov, WalkFov, 0.05);
		}
    }
}
