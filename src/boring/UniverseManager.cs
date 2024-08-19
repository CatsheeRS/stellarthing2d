using System;
using System.Collections.Generic;
using Godot;

namespace stellarthing;

/// <summary>
/// internal manager for save stuff
/// </summary>
public partial class UniverseManager : Node
{
    /// <summary>
	/// name of the current universe, used for saving
	/// </summary>
	public static string CurrentUniverse { get; set; } = "";
	public static string UniverseDir { get => $"user://universes/{CurrentUniverse}"; }
    static Dictionary<string, Variant> Properties { get; set; } = [];

    /// <summary>
    /// gets a property. if the type can't be a variant, serialize it as json
    /// </summary>
    public static T GetProperty<[MustBeVariant] T>(string prop)
    {
        if (!Properties.TryGetValue(prop, out Variant value)) {
            ConfigFile config = new();
            config.Load($"{UniverseDir}/universe.cfg");
            
            if (config.HasSectionKey("universe", prop)) {
                var stuff = config.GetValue("universe", prop, Universe.DefaultValues[prop]);
                Properties.Add(prop, stuff);
                return stuff.As<T>();
            }
            else {
                var stuff = Universe.DefaultValues[prop];
                Properties.Add(prop, stuff);
                return stuff.As<T>();
            }
        }
        else {
            return value.As<T>();
        }
    }

    /// <summary>
    /// sets a property. if the type can't be a variant, serialize it as json
    /// </summary>
    public static void SetProperty(string prop, Variant value)
    {
        // does the property even exist?
        if (!Properties.ContainsKey(prop)) {
            Properties.Add(prop, value);
        }
        else {
            Properties[prop] = value;
        }
    }

    /// <summary>
    /// saves the universe.
    /// </summary>
    public static void Save()
    {
        ConfigFile config = new();
        config.Load($"{UniverseDir}/universe.cfg");
        foreach (var prop in Properties) {
            config.SetValue("universe", prop.Key, prop.Value);
        }
        config.Save($"{UniverseDir}/universe.cfg");
    }
}