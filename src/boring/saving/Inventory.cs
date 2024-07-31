using System.Collections.Generic;
using System;
using Godot;

namespace stellarthing;

public class Inventory : IConfigData {
    public string GetFilename() => "%universe/inventory.json";
    public Dictionary<string, Item> Items { get; set; } = [];
}

public class Item {
    public string Scene { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string ModelPath { get; set; } = "";
    public Vector3 PreviewScale { get; set; } = Vector3.One;
    public Vector3 PreviewOffset { get; set; } = new Vector3(0, -1.5f, -2);
    public Vector3 ModelRotation { get; set; } = Vector3.Zero;
    public uint Amount { get; set; } = 0;
    public int Oxygen { get; set; } = 0;
    public int RocketFuel { get; set; } = 0;
    public int Uranium { get; set; } = 0;
    public int Gunpowder { get; set; } = 0;
    public int Silicon { get; set; } = 0;
    public int Iron { get; set; } = 0;
    public int Steel { get; set; } = 0;
    public int Titanium { get; set; } = 0;
    public int Diamond { get; set; } = 0;
    public int Ruby { get; set; } = 0;
    public int Emerald { get; set; } = 0;
    public int Sapphire { get; set; } = 0;
    public int Obsidian { get; set; } = 0;

    public override bool Equals(object obj)
    {
        if (obj is Item item) {
            return Scene == item.Scene && Name == item.Name && Description == item.Description &&
            ModelPath == item.ModelPath && PreviewScale.IsEqualApprox(item.PreviewScale) && Amount == item.Amount &&
            Oxygen == item.Oxygen && RocketFuel == item.RocketFuel && Uranium == item.Uranium &&
            Gunpowder == item.Gunpowder && Silicon == item.Silicon && Iron == item.Iron && Steel == item.Steel &&
            Titanium == item.Titanium && Diamond == item.Diamond && Ruby == item.Ruby && Emerald == item.Emerald &&
            Sapphire == item.Sapphire && Obsidian == item.Obsidian;
        }
        else return false;
    }

    public override int GetHashCode()
    {
        return Scene.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ ModelPath.GetHashCode() ^
        PreviewScale.GetHashCode() ^ Amount.GetHashCode() ^ Oxygen.GetHashCode() ^ RocketFuel.GetHashCode() ^
        Uranium.GetHashCode() ^ Gunpowder.GetHashCode() ^ Silicon.GetHashCode() ^ Iron.GetHashCode() ^
        Steel.GetHashCode() ^ Titanium.GetHashCode() ^ Diamond.GetHashCode() ^ Ruby.GetHashCode() ^
        Emerald.GetHashCode() ^ Sapphire.GetHashCode() ^ Obsidian.GetHashCode();
    }
}