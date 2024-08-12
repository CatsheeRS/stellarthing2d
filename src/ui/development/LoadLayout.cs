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
            q.AddToGroup("spaceship_furniture");

            // apply metadata
            foreach (var pp in p.Metadata) {
                q.SetMeta(pp.Key, pp.Value);
            }

            g.CallDeferred(MethodName.AddChild, q);
        }
    }
}
