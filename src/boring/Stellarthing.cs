using Godot;
using System;

namespace stellarthing;

/// <summary>
/// handsome singleton for crap
/// </summary>
public partial class Stellarthing : Node {
	AudioStreamPlayer player = new() {
		Stream = GD.Load<AudioStream>("res://assets/sounds/one_synth_note.mp3"),
		Bus = "ui",
	};
	Timer autosave = new() {
		WaitTime = 30,
		Autostart = true
	};

    public override void _Ready()
    {
        AddChild(player);
		AddChild(autosave);
		autosave.Timeout += UniverseManager.Save;

		// better for testing
		#if DEBUG
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		#endif
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("click")) {
			// i know
			var lol = GetViewport().GuiGetFocusOwner();
			if (lol != null) {
				if (lol is BaseButton) {
					if (!lol.HasMeta(MetaKeys.DontPlayButtonSound)) {
						player.Play();
					}
				}
			}
		}
    }

	/// <summary>
	/// very cool wrapper around ConfigFile so it's more convenient. use with ConfigSections and ConfigKeys. type must be able to convert to a Variant, if it can't, serialize it as json.
	/// </summary>
	public static T GetConfig<[MustBeVariant] T>(string section, string key, Variant defaultval = default)
	{
		ConfigFile config = new();
        config.Load("user://prefs.cfg");
		return config.GetValue(section, key, defaultval).As<T>();
	}
	
	/// <summary>
	/// very cool wrapper around ConfigFile so it's more convenient. use with ConfigSections and ConfigKeys. type must be able to convert to a Variant, if it can't, serialize it as json.
	/// </summary>
	public static void SetConfig(string section, string key, Variant value)
	{
		ConfigFile config = new();
        config.Load("user://prefs.cfg");
		config.SetValue(section, key, value);
		config.Save("user://prefs.cfg");
	}
}
