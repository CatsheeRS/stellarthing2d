using Godot;
using System;

namespace stellarthing;

public partial class NewUniverse : Button {
	[Export]
	public LineEdit UniverseName { get; set; }
	[Export]
	public PackedScene UniverseScene { get; set; }
    [Export]
    public PackedScene HudStuff { get; set; }
	[Export]
	public Control Lol { get; set; }

    public override void _Pressed()
    {
        UniverseManager.CurrentUniverse = UniverseName.Text != "" ? UniverseName.Text : "New Universe";

		// i'n prog rame
		UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace(">", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace(":", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace("\\", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace("/", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace("?", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace("*", "!");
        UniverseManager.CurrentUniverse = UniverseManager.CurrentUniverse.Replace(".", "!");

		DirAccess.MakeDirRecursiveAbsolute($"user://universes/{UniverseManager.CurrentUniverse}");
        // comically large line to create the funni file
        using var _ = FileAccess.Open($"user://universes/{UniverseManager.CurrentUniverse}/universe.cfg", FileAccess.ModeFlags.Write);
		GetTree().Root.AddChild(UniverseScene.Instantiate());
        GetTree().Root.AddChild(HudStuff.Instantiate());
		Lol.QueueFree();
    }
}
