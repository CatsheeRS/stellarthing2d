using Godot;
using System;

namespace stellarthing;

public partial class Hotbar : HBoxContainer
{
	[Export]
	public TextureRect[] Spots { get; set; }
	public static int CurrentSpot { get; set; } = 0;
	Color activeColor = new(1, 1, 1, 0.8f);
	Color inactiveColor = new(1, 1, 1, 0.5f);

    public override void _Process(double delta)
    {
		Spots[CurrentSpot].Modulate = inactiveColor;

        // change shit by scrolling
		if (Input.IsActionJustPressed("hotbar_left")) CurrentSpot--;
		if (Input.IsActionJustPressed("hotbar_right")) CurrentSpot++;

		// change shit by shortcuts, very efficient
		if (Input.IsPhysicalKeyPressed(Key.Shift)) {
			if (Input.IsPhysicalKeyPressed(Key.Key1)) CurrentSpot = 10;
			if (Input.IsPhysicalKeyPressed(Key.Key2)) CurrentSpot = 11;
			if (Input.IsPhysicalKeyPressed(Key.Key3)) CurrentSpot = 12;
			if (Input.IsPhysicalKeyPressed(Key.Key4)) CurrentSpot = 13;
			if (Input.IsPhysicalKeyPressed(Key.Key5)) CurrentSpot = 14;
			if (Input.IsPhysicalKeyPressed(Key.Key6)) CurrentSpot = 15;
			if (Input.IsPhysicalKeyPressed(Key.Key7)) CurrentSpot = 16;
			if (Input.IsPhysicalKeyPressed(Key.Key8)) CurrentSpot = 17;
			if (Input.IsPhysicalKeyPressed(Key.Key9)) CurrentSpot = 18;
			if (Input.IsPhysicalKeyPressed(Key.Key0)) CurrentSpot = 19;
		}
		else {
			if (Input.IsPhysicalKeyPressed(Key.Key1)) CurrentSpot = 0;
			if (Input.IsPhysicalKeyPressed(Key.Key2)) CurrentSpot = 1;
			if (Input.IsPhysicalKeyPressed(Key.Key3)) CurrentSpot = 2;
			if (Input.IsPhysicalKeyPressed(Key.Key4)) CurrentSpot = 3;
			if (Input.IsPhysicalKeyPressed(Key.Key5)) CurrentSpot = 4;
			if (Input.IsPhysicalKeyPressed(Key.Key6)) CurrentSpot = 5;
			if (Input.IsPhysicalKeyPressed(Key.Key7)) CurrentSpot = 6;
			if (Input.IsPhysicalKeyPressed(Key.Key8)) CurrentSpot = 7;
			if (Input.IsPhysicalKeyPressed(Key.Key9)) CurrentSpot = 8;
			if (Input.IsPhysicalKeyPressed(Key.Key0)) CurrentSpot = 9;
		}

		// loop around and stuff
		if (CurrentSpot < 0) CurrentSpot = 19;
		if (CurrentSpot > 19) CurrentSpot = 0;

		// actually update
		Spots[CurrentSpot].Modulate = activeColor;
    }
}
