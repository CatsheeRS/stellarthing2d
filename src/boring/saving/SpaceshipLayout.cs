using System.Collections.Generic;
using System;
using Godot;

namespace stellarthing;

public class SpaceshipLayout : IConfigData {
    public string GetFilename() => "%universe/spaceship_layout.json";
    public List<Furniture> Stuff { get; set; } = [];
}