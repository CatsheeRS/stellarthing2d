using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stellarthing;

public partial class ListInventoryFlucklery : VBoxContainer {
    [Export]
    public PackedScene Sceeneehee { get; set; }
    [Export]
    public Control Inve { get; set; }
    [Export]
    public AudioStreamPlayer FunniSoundy { get; set; }
    Config<Inventory> pastConfig = new();
    Node3D h;
    Item hh;
    string hhh;
    Label instruction;

    public override void _Ready()
    {
        instruction = GetNode<Label>("/root/hud/instruction_craft");
    }

    public void MurderersOfMurderers()
    {
        var config = new Config<Inventory>();

        // wouldn't be ideal to do node faffery every bloody second
        // that'd be bollocks mate
        // kill me
        DictionaryComparer<string, Item> whythough = new();
        if (whythough.Equals(config.Data.Items, pastConfig.Data.Items) && GetChildCount() > 1) {
            return;
        }

        // hehe
        Dictionary<string, Item> m = (
			from h in config.Data.Items
			orderby h.Value.Name ascending
			select h
		).ToDictionary();

        // do node faffery
        foreach (var g in GetChildren().Cast<Node>()) {
            if (g is HBoxContainer) g.QueueFree();
        }

        foreach (var mm in m) {
            if (mm.Value.Amount == 0) continue;
            
			Node mmm = Sceeneehee.Instantiate();
			var g = GD.Load<PackedScene>(mm.Value.ModelPath).Instantiate<Node3D>();
			g.Scale = mm.Value.PreviewScale;
			mmm.GetNode("picture/a/b").AddChild(g);
			mmm.GetNode<Label>("v/name").Text = Tr(mm.Value.Name);
			mmm.GetNode<Label>("v/description").Text = Tr(mm.Value.Description);
            mmm.GetNode<Label>("v/amount").Text = $"{mm.Value.Amount} {Tr("available")}";
			mmm.GetNode<Button>("v/place").Pressed += () => Jfjiwjijwjvwviwjviwjviwjviwdnvdvn(mm.Value, mm.Key);
			AddChild(mmm);
		}
    }

    void Jfjiwjijwjvwviwjviwjviwjviwdnvdvn(Item item, string key)
    {
        Inve.Visible = false;
        Inve.GetNode<Button>("../tool/bar/spaceship").ButtonPressed = false;
        var g = GetNode<Node3D>("/root/universe/preview_thing");
        h = GD.Load<PackedScene>(item.ModelPath).Instantiate<Node3D>();
        Player.OffsetThingy = item.PreviewOffset;
        h.Position = Player.ThingFafferyFuckeryThingy;
        h.Rotation = Player.ThingFafferyFuckeryThingyHehehehe;
        hh = item;
        hhh = key;
        g.AddChild(h);
    }

    public override void _Process(double delta)
    {
        if (h != null) {
            instruction.Visible = true;
            h.Position = Player.ThingFafferyFuckeryThingy;
            h.Rotation = Player.ThingFafferyFuckeryThingyHehehehe +
                new Vector3(
                    Mathf.DegToRad(hh.ModelRotation.X),
                    Mathf.DegToRad(hh.ModelRotation.Y),
                    Mathf.DegToRad(hh.ModelRotation.Z));
            
            if (Input.IsMouseButtonPressed(MouseButton.Left)) {
                h.QueueFree();
                var m = GD.Load<PackedScene>(hh.Scene).Instantiate<Node3D>();
                m.Position = h.Position;
                m.Rotation = h.Rotation - new Vector3(
                    Mathf.DegToRad(hh.ModelRotation.X),
                    Mathf.DegToRad(hh.ModelRotation.Y),
                    Mathf.DegToRad(hh.ModelRotation.Z));
                GetNode("/root/universe").AddChild(m);

                Config<Inventory> inv = new();
                inv.Data.Items[hhh].Amount--;
                inv.Save();

                Config<SpaceshipLayout> layout = new();
                layout.Data.Stuff.Add(new Furniture {
                    Position = m.Position,
                    Rotation = m.Rotation,
                    Item = hh,
                    Key = hhh
                });
                layout.Save();

                h = null;
                hh = null;
                hhh = "";
                Player.OffsetThingy = new Vector3(0, -1.5f, -2);
                FunniSoundy.Play();
            }

            // cancel
            if (Input.IsMouseButtonPressed(MouseButton.Right)) {
                h.QueueFree();
                h = null;
                hh = null;
                hhh = "";
                Player.OffsetThingy = new Vector3(0, -1.5f, -2);
            }
        }
        else {
            instruction.Visible = false;
        }
    }
}

/// <summary>
/// why
/// </summary>
class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>> {
    public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y) {
        return x.Count == y.Count && !x.Except(y).Any();
    }

    public int GetHashCode(Dictionary<TKey, TValue> obj) {
        int hash = 0;
        foreach (var pair in obj) {
            hash ^= pair.GetHashCode();
        }
        return hash;
    }
}