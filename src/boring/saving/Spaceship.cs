using System.Collections.Generic;
using Godot;

namespace stellarthing;

public class SpaceshipWalls : IConfigData {
    public string GetFilename() => "%universe/spaceship_walls.json";
    public Dictionary<Vector2, MaterialColor> Walls { get; set; }
}


public enum MaterialColor {
    White,
    Black,
    Red,
    Pink,
    Purple,
    Indigo,
    Blue,
    LightBlue,
    Cyan,
    Teal,
    Green,
    LightGreen,
    Lime,
    Yellow,
    Amber,
    Orange,
    DeepOrange,
    Brown,
    // i would use æ if it didn't look like shit on my font (monospaced brains on jetpacks)
    Graey,
    BlueGraey
}