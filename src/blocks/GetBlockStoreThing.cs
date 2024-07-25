using System.Collections.Generic;
using Godot;

namespace stellarthing;

public static class GetBlockStoreThing {
    public static Dictionary<string, Item> Fuckery { get; set; } = new() {
        {"chair", new() {
            Scene = "res://blocks/chair.tscn",
            Name = "Chair",
            Description = "Useful for sitting.",
            ModelPath = "res://assets/furniture/soft_chair.glb",
            Iron = 1,
        }},

        {"table", new() {
            Scene = "res://furniture/table.tscn",
            Name = "Table",
            Description = "Great for putting things on it.",
            ModelPath = "res://assets/furniture/table.glb",
            Iron = 1,
            PreviewScale = new Vector3(0.3f, 0.3f, 0.3f),
        }},

        {"fridge", new() {
            Scene = "res://furniture/fridge.tscn",
            Name = "Fridge",
            Description = "An amazing tool for storing food.",
            ModelPath = "res://assets/furniture/fridge.glb",
            Iron = 1,
            PreviewScale = new Vector3(0.75f, 0.75f, 0.75f),
        }}
    };
}