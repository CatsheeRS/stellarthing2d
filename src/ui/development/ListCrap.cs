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
			mmm.GetNode<Label>("v/name").Text = Tr(mm.Value.Name);
			mmm.GetNode<Label>("v/description").Text = Tr(mm.Value.Description);
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
				
				if (Resources.Iron < mm.Value.Iron) crapbutton.Disabled = true;
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

				if (Resources.Steel < mm.Value.Steel) crapbutton.Disabled = true;
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

				if (Resources.Titanium < mm.Value.Titanium) crapbutton.Disabled = true;
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

				if (Resources.Diamond < mm.Value.Diamond) crapbutton.Disabled = true;
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

				if (Resources.Ruby < mm.Value.Ruby) crapbutton.Disabled = true;
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

				if (Resources.Emerald < mm.Value.Emerald) crapbutton.Disabled = true;
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

				if (Resources.Sapphire < mm.Value.Sapphire) crapbutton.Disabled = true;
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

				if (Resources.Obsidian < mm.Value.Obsidian) crapbutton.Disabled = true;
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
				
				if (Resources.Gunpowder < mm.Value.Gunpowder) crapbutton.Disabled = true;
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

				if (Resources.Silicon < mm.Value.Silicon) crapbutton.Disabled = true;
			}

			crapbutton.Pressed += () => BuildStuff(mm.Key, mm.Value, crapbutton);
			AwesomeTimer.Timeout += () => {
				if (Resources.Iron < mm.Value.Iron) crapbutton.Disabled = true;
				if (Resources.Steel < mm.Value.Steel) crapbutton.Disabled = true;
				if (Resources.Titanium < mm.Value.Titanium) crapbutton.Disabled = true;
				if (Resources.Diamond < mm.Value.Diamond) crapbutton.Disabled = true;
				if (Resources.Ruby < mm.Value.Ruby) crapbutton.Disabled = true;
				if (Resources.Emerald < mm.Value.Emerald) crapbutton.Disabled = true;
				if (Resources.Sapphire < mm.Value.Sapphire) crapbutton.Disabled = true;
				if (Resources.Obsidian < mm.Value.Obsidian) crapbutton.Disabled = true;
				if (Resources.Gunpowder < mm.Value.Gunpowder) crapbutton.Disabled = true;
				if (Resources.Silicon < mm.Value.Silicon) crapbutton.Disabled = true;
			};
			AddChild(mmm);
		}
    }

	void BuildStuff(string id, Item item, Button crapbutton)
	{
		// add to inventory
		if (SpaceshipFurniture.Inventory.TryGetValue(id, out Item value)) {
            value.Amount++;
		}
		else {
			item.Amount = 1;
			SpaceshipFurniture.Inventory.Add(id, item);
		}

		// kill
		Resources.Iron -= item.Iron;
		Resources.Steel -= item.Steel;
		Resources.Titanium -= item.Titanium;
		Resources.Diamond -= item.Diamond;
		Resources.Ruby -= item.Ruby;
		Resources.Emerald -= item.Emerald;
		Resources.Sapphire -= item.Sapphire;
		Resources.Obsidian -= item.Obsidian;
		Resources.Gunpowder -= item.Gunpowder;
		Resources.Silicon -= item.Silicon;

		// czech republic
		if (Resources.Iron < item.Iron) crapbutton.Disabled = true;
		if (Resources.Steel < item.Steel) crapbutton.Disabled = true;
		if (Resources.Titanium < item.Titanium) crapbutton.Disabled = true;
		if (Resources.Diamond < item.Diamond) crapbutton.Disabled = true;
		if (Resources.Ruby < item.Ruby) crapbutton.Disabled = true;
		if (Resources.Emerald < item.Emerald) crapbutton.Disabled = true;
		if (Resources.Sapphire < item.Sapphire) crapbutton.Disabled = true;
		if (Resources.Obsidian < item.Obsidian) crapbutton.Disabled = true;
		if (Resources.Gunpowder < item.Gunpowder) crapbutton.Disabled = true;
		if (Resources.Silicon < item.Silicon) crapbutton.Disabled = true;
	}
}
