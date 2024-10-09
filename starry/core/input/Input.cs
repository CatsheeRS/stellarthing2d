using System;
using System.Collections.Generic;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things input
/// </summary>
public static class Input {
    /// <summary>
    /// 0 = not pressed, 1 = just pressed, 2 = pressed, 3 = just released
    /// </summary>
    internal static Dictionary<Key, byte> keystates = [];
    /// <summary>
    /// left, middle, right, respectively (i'm pretty sure)
    /// </summary>
    internal static byte[] mousefuckingstate = [inactive, inactive, inactive];
    internal const byte inactive = 0;
    internal const byte justPressed = 1;
    internal const byte pressed = 2;
    internal const byte justReleased = 3;

    public static vec2i mousePosition { get; internal set; }

    /// <summary>
    /// if true, the key is currently being held
    /// </summary>
    public static bool isKeyPressed(Key key) => keystates[key] != inactive;
    /// <summary>
    /// if true, the key just started being pressed
    /// </summary>
    public static bool isKeyJustPressed(Key key) => keystates[key] == justPressed;
    /// <summary>
    /// if true, the key just stopped being pressed
    /// </summary>
    public static bool isKeyReleased(Key key) => keystates[key] == justReleased;
    /// <summary>
    /// if true, the key isn't pressed at all
    /// </summary>
    public static bool isKeyNotPressed(Key key) => keystates[key] == inactive;

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
    public static bool isMouseButtonPressed(MouseButton button) => mousefuckingstate[(byte)button] != inactive;

    /// <summary>
    /// if true, the button just started being pressed
    /// </summary>
    public static bool isMouseButtonJustPressed(MouseButton button) => mousefuckingstate[(byte)button] == justPressed;

    /// <summary>
    /// if true, the button just stopped being pressed
    /// </summary>
    public static bool isMouseButtonReleased(MouseButton button) => mousefuckingstate[(byte)button] == justReleased;
    
    /// <summary>
    /// if true, the button isn't pressed at all
    /// </summary>
    public static bool isMouseButtonNotPressed(MouseButton button) => mousefuckingstate[(byte)button] == inactive;
}

public enum MouseButton
{
    left,
    middle,
    right
}