using System.Collections.Generic;
using System;
using Godot;

namespace stellarthing;

public class SpaceshipLayout : IConfigData {
    public string GetFilename() => "%universe/spaceship_layout.json";
    public List<Furniture> Stuff { get; set; } = [];
}

public class Furniture
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public object ExtraData { get; set; }
    public Item Item { get; set; }
}