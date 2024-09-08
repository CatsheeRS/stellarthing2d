using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things input
/// </summary>
public static class Input {
    public static vec2i mousePosition { get => vec2i(Raylib.GetMouseX(), Raylib.GetMouseY()); }

    /// <summary>
    /// if true, the key is currently being held
    /// </summary>
    public static bool isKeyPressed(Key key) => Raylib.IsKeyDown((KeyboardKey)(uint)key);
    /// <summary>
    /// if true, the key just started being pressed
    /// </summary>
    public static bool isKeyJustPressed(Key key) => Raylib.IsKeyPressed((KeyboardKey)(uint)key);
    /// <summary>
    /// if true, the key just stopped being pressed
    /// </summary>
    public static bool isKeyReleased(Key key) => Raylib.IsKeyReleased((KeyboardKey)(uint)key);
    /// <summary>
    /// similar to isKeyPressed(), but intended for text input
    /// </summary>
    public static bool isKeyTextRepeated(Key key) => Raylib.IsKeyPressedRepeat((KeyboardKey)(uint)key);
    /// <summary>
    /// if true, the key isn't pressed at all
    /// </summary>
    public static bool isKeyNotPressed(Key key) => Raylib.IsKeyUp((KeyboardKey)(uint)key);

    /// <summary>
    /// if true, the keymap is currently being held
    /// </summary>
    public static bool isKeymapPressed(string keymap)
    {
        foreach (var elmierda in settings.keymap[keymap]) {
            if (isKeyPressed(elmierda)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// if true, the keymap just started being pressed
    /// </summary>
    public static bool isKeymapJustPressed(string keymap)
    {
        foreach (var elmierda in settings.keymap[keymap]) {
            if (isKeyJustPressed(elmierda)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// if true, the keymap just stopped being pressed
    /// </summary>
    public static bool isKeymapReleased(string keymap)
    {
        foreach (var elmierda in settings.keymap[keymap]) {
            if (isKeyReleased(elmierda)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// similar to isKeymapPressed(), but intended for text input
    /// </summary>
    public static bool isKeymapTextRepeated(string keymap)
    {
        foreach (var elmierda in settings.keymap[keymap]) {
            if (isKeyTextRepeated(elmierda)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// if true, the keymap isn't pressed at all
    /// </summary>
    public static bool isKeymapNotPressed(string keymap)
    {
        foreach (var elmierda in settings.keymap[keymap]) {
            if (isKeyNotPressed(elmierda)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// if true, the button is currently being held
    /// </summary>
    public static bool isMouseButtonPressed(MouseButton button)
    {
        return button switch {
            MouseButton.left => Raylib.IsMouseButtonDown(Raylib_cs.MouseButton.Left),
            MouseButton.middle => Raylib.IsMouseButtonDown(Raylib_cs.MouseButton.Middle),
            MouseButton.right => Raylib.IsMouseButtonDown(Raylib_cs.MouseButton.Right),
            _ => throw new Exception(),
        };
    }

    /// <summary>
    /// if true, the button just started being pressed
    /// </summary>
    public static bool isMouseButtonJustPressed(MouseButton button)
    {
        return button switch {
            MouseButton.left => Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.Left),
            MouseButton.middle => Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.Middle),
            MouseButton.right => Raylib.IsMouseButtonPressed(Raylib_cs.MouseButton.Right),
            _ => throw new Exception(),
        };
    }

    /// <summary>
    /// if true, the button just stopped being pressed
    /// </summary>
    public static bool isMouseButtonReleased(MouseButton button)
    {
        return button switch {
            MouseButton.left => Raylib.IsMouseButtonReleased(Raylib_cs.MouseButton.Left),
            MouseButton.middle => Raylib.IsMouseButtonReleased(Raylib_cs.MouseButton.Middle),
            MouseButton.right => Raylib.IsMouseButtonReleased(Raylib_cs.MouseButton.Right),
            _ => throw new Exception(),
        };
    }

    /// <summary>
    /// if true, the button isn't pressed at all
    /// </summary>
    public static bool isMouseButtonNotPressed(MouseButton button)
    {
        return button switch {
            MouseButton.left => Raylib.IsMouseButtonUp(Raylib_cs.MouseButton.Left),
            MouseButton.middle => Raylib.IsMouseButtonUp(Raylib_cs.MouseButton.Middle),
            MouseButton.right => Raylib.IsMouseButtonUp(Raylib_cs.MouseButton.Right),
            _ => throw new Exception(),
        };
    }
}

public enum MouseButton
{
    left,
    middle,
    right
}