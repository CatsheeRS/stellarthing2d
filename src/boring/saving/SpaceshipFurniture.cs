using Godot;
using System;
using System.Collections.Generic;

namespace stellarthing;

public class FurnitureStuff {
    public Dictionary<string, Item> Inventory { get; set; } = [];
    public List<Item> Furniture { get; set; } = [];
}

public class Furniture
{
    public string Key { get; set; } = "";
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Dictionary<string, Variant> Metadata { get; set; } = [];
    public Item Item { get; set; }
}