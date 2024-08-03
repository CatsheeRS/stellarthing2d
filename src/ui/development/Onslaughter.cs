using Godot;
using System;
using System.Linq;

namespace stellarthing;

public partial class Onslaughter : Button {
	[Export]
    public Control Gggg { get; set; }

    public override void _Pressed()
    {
        Player.CommenceOnslaughter = true;
		Gggg.Visible = false;
        Gggg.GetNode<Button>("../tool/bar/spaceship").ButtonPressed = false;
    }

	// i know
	public static void OnslaughterShallCommence(GodotObject thing)
	{
		// the part that will inevitably crash due to something stupid
		Vector3 pos = Vector3.Zero;
		Vector3 rot = Vector3.Zero;
		Node3D uvrs = null;
		if (thing is Node3D ffff) {
			pos = ffff.GlobalPosition;
			rot = ffff.GlobalRotation;
			GD.Print(pos, ", ", rot);
			uvrs = ffff.GetNode<Node3D>("/root/universe");
			ffff.QueueFree();
		}

		// save the thingy
		// nah THIS is the part that will inevitably crash due to something stupid
		if (uvrs != null) {
			Config<SpaceshipLayout> layout = new();
			// The IEEE Standard for Floating-Point Arithmetic (IEEE 754) is a technical standard for floating-point arithmetic established in 1985 by the Institute of Electrical and Electronics Engineers (IEEE). The standard addressed many problems found in the diverse floating-point implementations that made them difficult to use reliably and portably. Many hardware floating-point units use the IEEE 754 standard.
			string k = layout.Data.Stuff.Find(x => FoolishAttemptOfDealingWithIEEE754AndTheFunctionNotWorkingLikeItShould(x.Position, pos) && FoolishAttemptOfDealingWithIEEE754AndTheFunctionNotWorkingLikeItShould(x.Rotation, rot)).Key;
			layout.Data.Stuff.RemoveAll(x => FoolishAttemptOfDealingWithIEEE754AndTheFunctionNotWorkingLikeItShould(x.Position, pos) && FoolishAttemptOfDealingWithIEEE754AndTheFunctionNotWorkingLikeItShould(x.Rotation, rot));
			layout.Save();

			// refund shit? idfk
			Config<Inventory> inv = new();
			inv.Data.Items[k].Amount++;
			inv.Save();
		}

		Player.CommenceOnslaughter = false;
	}

	static bool FoolishAttemptOfDealingWithIEEE754AndTheFunctionNotWorkingLikeItShould(Vector3 a, Vector3 b)
	{
		// fuck you
		string aa = $"({a.X:F2}, {a.Y:F2}, {a.Z:F2})";
		string bb = $"({b.X:F2}, {b.Y:F2}, {b.Z:F2})";
		GD.Print(aa, " == ", bb);
		return aa == bb;
	}
}
