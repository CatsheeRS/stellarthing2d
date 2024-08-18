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
	public static string UniverseDir { get => $"user://universes/{CurrentUniverse}"; }

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
