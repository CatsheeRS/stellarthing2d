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
		Vector3 pos = Vector3.Inf;
		Vector3 rot = Vector3.Inf;
		Node3D uvrs = null;
		if (thing is Node3D ffff) {
			pos = ffff.Position;
			rot = ffff.Rotation;
			GD.Print(pos, ", ", rot);
			uvrs = ffff.GetNode<Node3D>("/root/universe");
			ffff.QueueFree();
		}

		// save the thingy
		// nah THIS is the part that will inevitably crash due to something stupid
		if (uvrs != null) {
			Config<SpaceshipLayout> layout = new();
			string k = layout.Data.Stuff.Find(x => x.Position.IsEqualApprox(pos) && x.Rotation.IsEqualApprox(rot)).Key;
			layout.Data.Stuff.RemoveAll(x => x.Position.IsEqualApprox(pos) && x.Rotation.IsEqualApprox(rot));
			layout.Save();

			// refund shit? idfk
			Config<Inventory> inv = new();
			inv.Data.Items[k].Amount++;
			inv.Save();
		}

		Player.CommenceOnslaughter = false;
	}
}
