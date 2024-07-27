using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stellarthing;

public partial class ListInventoryFlucklery : VBoxContainer {
    [Export]
    public PackedScene Sceeneehee { get; set; }
    Config<Inventory> pastConfig = new();

    public void MurderersOfMurderers()
    {
        var config = new Config<Inventory>();

        // wouldn't be ideal to do node faffery every bloody second
        // that'd be bollocks mate
        // kill me
        GD.Print("g kgglg");
        DictionaryComparer<string, Item> whythough = new();
        if (whythough.Equals(config.Data.Items, pastConfig.Data.Items) && GetChildCount() > 1) {
            GD.Print("g");
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
            if (g is not Timer) g.QueueFree();
        }

        foreach (var mm in m) {
			Node mmm = Sceeneehee.Instantiate();
			var g = GD.Load<PackedScene>(mm.Value.ModelPath).Instantiate<Node3D>();
			g.Scale = mm.Value.PreviewScale;
			mmm.GetNode("picture/a/b").AddChild(g);
			mmm.GetNode<Label>("v/name").Text = Tr(mm.Value.Name);
			mmm.GetNode<Label>("v/description").Text = Tr(mm.Value.Description);
            mmm.GetNode<Label>("v/amount").Text = $"{mm.Value.Amount} {Tr("available")}";
			mmm.GetNode<Button>("v/place").Pressed += () => Jfjiwjijwjvwviwjviwjviwjviwdnvdvn(mm.Value);
			AddChild(mmm);
		}
    }

    void Jfjiwjijwjvwviwjviwjviwjviwdnvdvn(Item item)
    {
        GD.Print("dfdfdof4rbfsa´b,r/wt4w[\\w2]");
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