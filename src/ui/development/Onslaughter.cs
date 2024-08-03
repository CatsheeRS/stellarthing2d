using Godot;
using System;
using System.Globalization;
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
			string k = layout.Data.Stuff.Find(x => THEREISNOESCAPE(x.Position, pos, false) && THEREISNOESCAPE(x.Rotation, rot, true)).Key;

			layout.Data.Stuff.RemoveAll(x => THEREISNOESCAPE(x.Position, pos, false) && THEREISNOESCAPE(x.Rotation, rot, true));
			layout.Save();

			// refund shit? idfk
			Config<Inventory> inv = new();
			inv.Data.Items[k].Amount++;
			inv.Save();
		}

		Player.CommenceOnslaughter = false;
	}

	static bool THEREISNOESCAPE(Vector3 a, Vector3 b, bool includeY)
	{
		if (!includeY) {
			a = new Vector3(a.X, 0, a.Z);
			b = new Vector3(b.X, 0, b.Z);
		}

		// fuck you
		string aa = $"({a.X.ToString("F2", CultureInfo.InvariantCulture)}, {a.Y.ToString("F2", CultureInfo.InvariantCulture)}, {a.Z.ToString("F2", CultureInfo.InvariantCulture)})".Replace("-0", "0");
		string bb = $"({b.X.ToString("F2", CultureInfo.InvariantCulture)}, {b.Y.ToString("F2", CultureInfo.InvariantCulture)}, {b.Z.ToString("F2", CultureInfo.InvariantCulture)})".Replace("-0", "0");
		GD.Print(aa, " == ", bb);
		return aa == bb;
	}
}
