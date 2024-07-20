using Godot;

namespace stellarthing;

public class Resources : IConfigData {
    public string GetFilename() => "%universe/resources.json";
    public int Oxygen { get; set; } = 0;
    public int Water { get; set; } = 0;
    public int RocketFuel { get; set; } = 0;
    public int Uranium { get; set; } = 0;
    public int Gunpowder { get; set; } = 0;
    public int Silicon { get; set; } = 0;
    public int Iron { get; set; } = 0;
    public int Steel { get; set; } = 0;
    public int Titanium { get; set; } = 0;
    public int Diamond { get; set; } = 0;
    public int Ruby { get; set; } = 0;
    public int Emerald { get; set; } = 0;
    public int Sapphire { get; set; } = 0;
    public int Obsidian { get; set; } = 0;
}
