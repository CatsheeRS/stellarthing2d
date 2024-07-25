using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stellarthing;

public partial class ListCrap : VBoxContainer {
	[Export]
	public PackedScene Sceeneehee { get; set; }
	[Export]
	public string Faffery { get; set; } = "decoration";
	[Export]
	public Texture2D Iron { get; set; }
	[Export]
	public Texture2D Steel { get; set; }
	[Export]
	public Texture2D Titanium { get; set; }
	[Export]
	public Texture2D Diamond { get; set; }
	[Export]
	public Texture2D Ruby { get; set; }
	[Export]
	public Texture2D Emerald { get; set; }
	[Export]
	public Texture2D Sapphire { get; set; }
	[Export]
	public Texture2D Obsidian { get; set; }
	[Export]
	public Texture2D Gunpowder { get; set; }
	[Export]
	public Texture2D Silicon { get; set; }
	[Export]
	public Timer AwesomeTimer { get; set; }

	Config<Resources> cpfmdfgd = new();

    public override void _Ready()
    {
        Dictionary<string, Item> m = GetBlockStoreThing.Fuckery;
		m = (
			from h in m
			orderby h.Value.Name ascending
			select h
		).ToDictionary();

		foreach (var mm in m) {
			Node mmm = Sceeneehee.Instantiate();
			var g = GD.Load<PackedScene>(mm.Value.ModelPath).Instantiate<Node3D>();
			g.Scale = mm.Value.PreviewScale;
			mmm.GetNode("picture/a/b").AddChild(g);
			mmm.GetNode<Label>("v/name").Text = mm.Value.Name;
			mmm.GetNode<Label>("v/description").Text = mm.Value.Description;
			Node why = mmm.GetNode("v/resources");
			var crapbutton = mmm.GetNode<Button>("v/build");

			// misery
			if (mm.Value.Iron != 0) {
				why.AddChild(new TextureRect {
					Texture = Iron,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Iron.ToString()
				});
				
				if (cpfmdfgd.Data.Iron < mm.Value.Iron) crapbutton.Disabled = true;
			}

			if (mm.Value.Steel != 0) {
				why.AddChild(new TextureRect {
					Texture = Steel,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Steel.ToString()
				});

				if (cpfmdfgd.Data.Steel < mm.Value.Steel) crapbutton.Disabled = true;
			}

			if (mm.Value.Titanium != 0) {
				why.AddChild(new TextureRect {
					Texture = Titanium,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Titanium.ToString()
				});

				if (cpfmdfgd.Data.Titanium < mm.Value.Titanium) crapbutton.Disabled = true;
			}

			if (mm.Value.Diamond != 0) {
				why.AddChild(new TextureRect {
					Texture = Diamond,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Titanium.ToString()
				});

				if (cpfmdfgd.Data.Diamond < mm.Value.Diamond) crapbutton.Disabled = true;
			}

			if (mm.Value.Ruby != 0) {
				why.AddChild(new TextureRect {
					Texture = Ruby,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Ruby.ToString()
				});

				if (cpfmdfgd.Data.Ruby < mm.Value.Ruby) crapbutton.Disabled = true;
			}

			if (mm.Value.Emerald != 0) {
				why.AddChild(new TextureRect {
					Texture = Emerald,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Emerald.ToString()
				});

				if (cpfmdfgd.Data.Emerald < mm.Value.Emerald) crapbutton.Disabled = true;
			}

			if (mm.Value.Sapphire != 0) {
				why.AddChild(new TextureRect {
					Texture = Sapphire,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Sapphire.ToString()
				});

				if (cpfmdfgd.Data.Sapphire < mm.Value.Sapphire) crapbutton.Disabled = true;
			}

			if (mm.Value.Obsidian != 0) {
				why.AddChild(new TextureRect {
					Texture = Obsidian,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Obsidian.ToString()
				});

				if (cpfmdfgd.Data.Obsidian < mm.Value.Obsidian) crapbutton.Disabled = true;
			}

			if (mm.Value.Gunpowder != 0) {
				why.AddChild(new TextureRect {
					Texture = Gunpowder,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Gunpowder.ToString()
				});
				
				if (cpfmdfgd.Data.Gunpowder < mm.Value.Gunpowder) crapbutton.Disabled = true;
			}

			if (mm.Value.Silicon != 0) {
				why.AddChild(new TextureRect {
					Texture = Silicon,
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
					CustomMinimumSize = new Vector2(48, 48),
				});
				why.AddChild(new Label {
					Text = mm.Value.Silicon.ToString()
				});

				if (cpfmdfgd.Data.Silicon < mm.Value.Silicon) crapbutton.Disabled = true;
			}

			crapbutton.Pressed += () => BuildStuff(mm.Key, mm.Value, crapbutton);
			AwesomeTimer.Timeout += () => {
				if (cpfmdfgd.Data.Iron < mm.Value.Iron) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Steel < mm.Value.Steel) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Titanium < mm.Value.Titanium) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Diamond < mm.Value.Diamond) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Ruby < mm.Value.Ruby) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Emerald < mm.Value.Emerald) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Sapphire < mm.Value.Sapphire) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Obsidian < mm.Value.Obsidian) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Gunpowder < mm.Value.Gunpowder) crapbutton.Disabled = true;
				if (cpfmdfgd.Data.Silicon < mm.Value.Silicon) crapbutton.Disabled = true;
			};
			AddChild(mmm);
		}
    }

	void BuildStuff(string id, Item item, Button crapbutton)
	{
		// add to inventory
		Config<Inventory> config = new();
		if (config.Data.Items.TryGetValue(id, out Item value)) {
            value.Amount++;
		}
		else {
			item.Amount = 1;
			config.Data.Items.Add(id, item);
		}
		config.Save();

		// kill
		cpfmdfgd.Data.Iron -= item.Iron;
		cpfmdfgd.Data.Steel -= item.Steel;
		cpfmdfgd.Data.Titanium -= item.Titanium;
		cpfmdfgd.Data.Diamond -= item.Diamond;
		cpfmdfgd.Data.Ruby -= item.Ruby;
		cpfmdfgd.Data.Emerald -= item.Emerald;
		cpfmdfgd.Data.Sapphire -= item.Sapphire;
		cpfmdfgd.Data.Obsidian -= item.Obsidian;
		cpfmdfgd.Data.Gunpowder -= item.Gunpowder;
		cpfmdfgd.Data.Silicon -= item.Silicon;
		cpfmdfgd.Save();

		// czech republic
		if (cpfmdfgd.Data.Iron < item.Iron) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Steel < item.Steel) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Titanium < item.Titanium) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Diamond < item.Diamond) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Ruby < item.Ruby) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Emerald < item.Emerald) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Sapphire < item.Sapphire) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Obsidian < item.Obsidian) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Gunpowder < item.Gunpowder) crapbutton.Disabled = true;
		if (cpfmdfgd.Data.Silicon < item.Silicon) crapbutton.Disabled = true;
	}
}
