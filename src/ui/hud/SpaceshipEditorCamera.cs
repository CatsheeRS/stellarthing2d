using Godot;
using System;
using System.Collections.Generic;

namespace stellarthing;

public partial class SpaceshipEditorCamera : Camera2D
{
	[Export]
	public Sprite2D MyProperty { get; set; }
	Dictionary<Vector2, TextureRect> funniCrap = [];

    public override void _Input(InputEvent @event)
    {
		if (Input.IsActionJustPressed("move_left")) Position += new Vector2(-10, 0);
		if (Input.IsActionJustPressed("move_right")) Position += new Vector2(10, 0);
		if (Input.IsActionJustPressed("move_down")) Position += new Vector2(0, 10);
		if (Input.IsActionJustPressed("move_up")) Position += new Vector2(0, -10);

        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			if (!funniCrap.ContainsKey(MyProperty.GlobalPosition)) {
				TextureRect j = new() {
					Texture = GD.Load<Texture2D>("res://assets/ui/green_square.png"),
					Position = MyProperty.GlobalPosition,
					CustomMinimumSize = new Vector2(10, 10),
				};
				GetParent().AddChild(j);
				funniCrap.Add(MyProperty.GlobalPosition, j);
			}
		}

		if (Input.IsMouseButtonPressed(MouseButton.Right)) {
			if (funniCrap.TryGetValue(MyProperty.GlobalPosition, out TextureRect value)) {
                value.QueueFree();
				funniCrap.Remove(MyProperty.GlobalPosition);
			}
		}

		if (Input.IsActionJustReleased("zoom_in")) {
			Zoom += new Vector2(0.5f, 0.5f);
		}

		if (Input.IsActionJustReleased("zoom_out") && Zoom > new Vector2(0.5f, 0.5f)) {
			Zoom -= new Vector2(0.5f, 0.5f);
		}
    }
}
