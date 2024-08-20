using Godot;
using System;

namespace stellarthing;

public partial class Camera : Node3D
{
	[Export]
	public Node3D YawNode { get; set; }
	[Export]
	public Node3D PitchNode { get; set; }
	[Export]
	public Camera3D Camerá { get; set; }
	[Export]
	public SpringArm3D SpringArm { get; set; }
	[Export]
	public Player Player { get; set; }
	[Export]
	public double PitchMin { get; set; } = -55;
	[Export]
	public double PitchMax { get; set; } = 75;
	[Export]
	public double NormalFov { get; set; } = 90;
	[Export]
	public double RunFov { get; set; } = 125;

	double yaw = 0;
	double pitch = 0;
	public double yawSensitivity = 0.07;
	public double pitchSensitivity = 0.07;
	double yawAcceleration = 15;
	double pitchAcceleration = 15;
	double currentFov = 90;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
		SpringArm.AddExcludedObject(Player.GetRid());
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion vent) {
			yaw += -vent.Relative.X * yawSensitivity;
			pitch += vent.Relative.Y * pitchSensitivity;
		}
    }

    public override void _PhysicsProcess(double delta)
    {
		pitch = Mathf.Clamp(pitch, PitchMin, PitchMax);

		// smooth camera
        // YawNode.RotationDegrees = new Vector3(0, (float)Mathf.Lerp(YawNode.RotationDegrees.Y, yaw,
		// 	yawAcceleration * delta), 0);
		
		// PitchNode.RotationDegrees = new Vector3((float)Mathf.Lerp(PitchNode.RotationDegrees.X, pitch,
		// 	pitchAcceleration * delta), 0, 0);

		// not smooth camera
		YawNode.RotationDegrees = new Vector3(0, (float)yaw, 0);
		PitchNode.RotationDegrees = new Vector3((float)pitch, 0, 0);

		if (Input.IsActionPressed("run")) currentFov = RunFov;
		else currentFov = NormalFov;

		Camerá.Fov = (float)Mathf.Lerp(Camerá.Fov, currentFov, 0.25);
    }
}
