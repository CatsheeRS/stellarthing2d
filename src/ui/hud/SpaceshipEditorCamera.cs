using Godot;
using System;

namespace stellarthing;

public partial class SpaceshipEditorCamera : Camera2D
{
    public override void _Input(InputEvent @event)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			Position = GetGlobalMousePosition();
		}

		if (Input.IsActionJustReleased("zoom_in")) {
			Zoom += new Vector2(0.5f, 0.5f);
		}

		if (Input.IsActionJustReleased("zoom_out") && Zoom > new Vector2(0.5f, 0.5f)) {
			Zoom -= new Vector2(0.5f, 0.5f);
		}
    }
}
