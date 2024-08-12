using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stellarthing;

public partial class UltraSaver5000 : Node {
    public override void _Ready()
    {
        Timer timer = new() {
            WaitTime = 30,
            Autostart = true,
            OneShot = false
        };
        timer.Timeout += Save;
        AddChild(timer);
    }

    public void Load()
    {

    }

    public void Save()
    {
        // why bother? it's gonna hurt me :(
        using var f = FileAccess.Open($"{Stellarthing.UniverseDir}/furniture.json", FileAccess.ModeFlags.Write);
        
        // this processes furniture
        List<Furniture> items = [];
        var yarr = GetTree().GetNodesInGroup("spaceship_furniture");
        foreach (var fufufu in yarr) {
            if (fufufu is Node3D node3) {
                // data but make it meta
                Dictionary<string, Variant> meta = [];
                foreach (var heheheha in node3.GetMetaList()) {
                    meta.Add(heheheha, node3.GetMeta(heheheha));
                }

                items.Add(new Furniture {
                    Key = (string)node3.GetMeta(MetaKeys.FurnitureKey),
                    Item = JsonConvert.DeserializeObject<Item>((string)node3.GetMeta(MetaKeys.FurnitureItem)),
                    Position = node3.Position,
                    Rotation = node3.Rotation,
                    Metadata = meta,
                });
            }
        }

        // save faffery furniture :D
        
        f.StoreString(JsonConvert.SerializeObject(items));
    }
}