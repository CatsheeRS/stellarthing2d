using Godot;
using System;

namespace stellarthing;

public partial class LoadLayout : Node {
    public override void _Ready()
    {
        Config<SpaceshipLayout> config = new();
        var g = GetParent();

        foreach (var p in config.Data.Stuff) {
            var q = GD.Load<PackedScene>(p.Item.Scene).Instantiate<Node3D>();
            q.Position = p.Position;
            q.Rotation = p.Rotation;
            g.CallDeferred(MethodName.AddChild, q);
        }
    }
}
