using System.Collections.Generic;
using Godot;

namespace stellarthing;

public static class GetBlockStoreThing {
    public static Dictionary<string, Item> Fuckery { get; set; } = new() {
        {"chair", new() {
            Scene = "res://furniture/soft_chair.tscn",
            Name = "Chair",
            Description = "Useful for sitting.",
            ModelPath = "res://assets/furniture/soft_chair.glb",
            PreviewScale = new Vector3(1, 1, 1),
            PreviewOffset = new Vector3(0, -3.5f, -2),
            Iron = 1,
        }},

        {"table", new() {
            Scene = "res://furniture/table.tscn",
            Name = "Table",
            Description = "Great for putting things on it.",
            ModelPath = "res://assets/furniture/table.glb",
            PreviewScale = new Vector3(0.3f, 0.3f, 0.3f),
            PreviewOffset = new Vector3(0, -2.5f, -3),
            Iron = 1,
        }},

        {"fridge", new() {
            Scene = "res://furniture/fridge.tscn",
            Name = "Fridge",
            Description = "An amazing tool for storing food.",
            ModelPath = "res://assets/furniture/fridge.glb",
            PreviewScale = new Vector3(0.75f, 0.75f, 0.75f),
            PreviewOffset = new Vector3(0, -1.5f, -2),
            ModelRotation = new Vector3(0, -1.571f, 0),
            Iron = 1,
        }}
    };
}