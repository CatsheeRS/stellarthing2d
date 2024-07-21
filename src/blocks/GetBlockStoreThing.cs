using System.Collections.Generic;

namespace stellarthing;

public static class GetBlockStoreThing {
    public static Dictionary<string, Item> Fuckery { get; set; } = new() {
        {"chair", new() {
            Scene = "res://blocks/chair.tscn",
            Name = "Chair",
            Image = "res://assets/blocks/chair.png",
            Iron = 1
        }}
    };
}