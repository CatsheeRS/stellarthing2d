using Godot;
using System;

namespace stellarthing;

public partial class NewUniverse : Button {
	[Export]
	public LineEdit UniverseName { get; set; }
	[Export]
	public PackedScene UniverseScene { get; set; }
	[Export]
	public Control Lol { get; set; }

    public override void _Pressed()
    {
        Stellarthing.CurrentUniverse = UniverseName.Text != "" ? UniverseName.Text : "New Universe";

		// i'n prog rame
		Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace(">", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace(":", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace("\\", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace("/", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace("?", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace("*", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace(".", "");
        Stellarthing.CurrentUniverse = Stellarthing.CurrentUniverse.Replace(" ", "");

		DirAccess.MakeDirRecursiveAbsolute($"user://universes/{Stellarthing.CurrentUniverse}");
		GetTree().Root.AddChild(UniverseScene.Instantiate());
		Lol.QueueFree();
    }
}
