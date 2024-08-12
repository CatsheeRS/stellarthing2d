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
        // furnitur
        SpaceshipFurniture.SaveVersion furniture = new();
        if (FileAccess.FileExists($"{Stellarthing.UniverseDir}/furniture.json")) {
            using var f = FileAccess.Open($"{Stellarthing.UniverseDir}/furniture.json", FileAccess.ModeFlags.Read);
            string furniturejson = f.GetAsText();
            furniture = JsonConvert.DeserializeObject<SpaceshipFurniture.SaveVersion>(furniturejson);
        }
        else {
            using var f = FileAccess.Open($"{Stellarthing.UniverseDir}/furniture.json", FileAccess.ModeFlags.Write);
            f.StoreString(JsonConvert.SerializeObject(furniture));
        }

        SpaceshipFurniture.Inventory = furniture.Inventory;
        SpaceshipFurniture.StartupFurniture = furniture.Furniture;
    }

    public void Save()
    {   
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

        // save faffery furniture
        // why bother? it's gonna hurt me :(
        using var f = FileAccess.Open($"{Stellarthing.UniverseDir}/furniture.json", FileAccess.ModeFlags.Write);
        f.StoreString(JsonConvert.SerializeObject(new SpaceshipFurniture.SaveVersion {
            Inventory = SpaceshipFurniture.Inventory,
            Furniture = items
        }));
    }
}