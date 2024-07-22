using Godot;
using System;
using System.Collections.Generic;

namespace stellarthing;

public partial class ListCrap : VBoxContainer {
	[Export]
	public PackedScene Sceeneehee { get; set; }
	[Export]
	public string Faffery { get; set; } = "decoration";
	// TODO: don't use placrholdersss
	readonly Texture2D lolIcon = GD.Load<Texture2D>("res://assets/placeholder/egg.png");

    public override void _Ready()
    {
        Dictionary<string, Item> m = GetBlockStoreThing.Fuckery;

		foreach (var mm in m) {
			Node mmm = Sceeneehee.Instantiate();
			mmm.GetNode<TextureRect>("picture").Texture = GD.Load<Texture2D>(mm.Value.Image);
			mmm.GetNode<Label>("v/name").Text = mm.Value.Name;
			mmm.GetNode<Label>("v/description").Text = mm.Value.Description;

			Node why = mmm.GetNode("v/resources");

			// misery
			if (mm.Value.Iron != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Iron.ToString()
				});
			}

			if (mm.Value.Steel != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Steel.ToString()
				});
			}

			if (mm.Value.Titanium != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Titanium.ToString()
				});
			}

			if (mm.Value.Diamond != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Diamond.ToString()
				});
			}

			if (mm.Value.Ruby != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Ruby.ToString()
				});
			}

			if (mm.Value.Emerald != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Emerald.ToString()
				});
			}

			if (mm.Value.Sapphire != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Sapphire.ToString()
				});
			}

			if (mm.Value.Obsidian != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Obsidian.ToString()
				});
			}

			if (mm.Value.Gunpowder != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Gunpowder.ToString()
				});
			}

			if (mm.Value.Silicon != 0) {
				why.AddChild(new TextureRect {
					Texture = lolIcon
				});
				why.AddChild(new Label {
					Text = mm.Value.Silicon.ToString()
				});
			}

			AddChild(mmm);
		}
    }
}
