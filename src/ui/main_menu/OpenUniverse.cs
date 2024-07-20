using Godot;
using System;

namespace stellarthing;

public partial class OpenUniverse : Button {
	[Export]
	public ItemList G { get; set; }
	[Export]
	public PackedScene UniverseScene { get; set; }
	[Export]
	public Control Lol { get; set; }

    public override void _Pressed()
    {
        Stellarthing.CurrentUniverse = G.GetItemText(G.GetSelectedItems()[0]);
		GetTree().Root.AddChild(UniverseScene.Instantiate());
		Lol.QueueFree();
    }
}
