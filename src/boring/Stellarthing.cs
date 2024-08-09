using Godot;
using System;

namespace stellarthing;

/// <summary>
/// handsome singleton for crap
/// </summary>
public partial class Stellarthing : Node {
	/// <summary>
	/// name of the current universe, used for saving
	/// </summary>
	public static string CurrentUniverse { get; set; } = "";
	AudioStreamPlayer player = new() {
		Stream = GD.Load<AudioStream>("res://assets/sounds/one_synth_note.mp3"),
		Bus = "ui",
	};

    public override void _Ready()
    {
        AddChild(player);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("click")) {
			var lol = GetViewport().GuiGetFocusOwner();
			if (lol != null) {
				if (lol is BaseButton h) {
					player.Play();
				}
			}
		}
    }
}
