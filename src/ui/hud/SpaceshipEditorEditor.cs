using Godot;
using System;

namespace stellarthing;

public partial class SpaceshipEditorEditor : SubViewport
{
	[Export]
	public Camera2D MyProperty { get; set; }

    public override void _Input(InputEvent @event)
    {
		if (@event is InputEventMouseButton ggg) {
			if (ggg.ButtonIndex != MouseButton.Right) {
				return;
			}

			Vector2 pos = GetMousePosition().Snapped(new Vector2(10, 10));
			AddChild(new TextureRect {
				Texture = GD.Load<Texture2D>("res://assets/ui/green_square.png"),
				Position = (pos + GetParent<Control>().GlobalPosition + new Vector2(1, 1)) * MyProperty.Zoom,
				CustomMinimumSize = new Vector2(10, 10),
			});
		}
    }
}
