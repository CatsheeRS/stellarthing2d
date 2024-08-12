using Godot;
using System;
using System.Collections.Generic;

namespace stellarthing;

public static class SpaceshipFurniture {
    /// <summary>
    /// all furniture the player can place
    /// </summary>
    public static Dictionary<string, Item> Inventory { get; set; } = [];
    /// <summary>
    /// the furniture at startup, used solely for loading stuff at the start
    /// </summary>
    public static List<Furniture> StartupFurniture { get; set; } = [];

    public class SaveVersion {
        public Dictionary<string, Item> Inventory { get; set; } = [];
        public List<Furniture> Furniture { get; set; } = [];
    }
}

public class Furniture
{
    public string Key { get; set; } = "";
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Dictionary<string, Variant> Metadata { get; set; } = [];
    public Item Item { get; set; }
}