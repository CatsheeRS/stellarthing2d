using System;
using System.Linq;
using static starry.Starry;

namespace starry;

/// <summary>
/// trigged when a key is pressed
/// </summary>
public class KeypressEvent : IInputEvent {
    public InputType getType() => InputType.keypress;

    /// <summary>
    /// the code for the key
    /// </summary>
    public Key keycode { get; set; }
    public KeypressType type { get; set; }
    public bool isKeymap(string key) => settings.keymap[key].Contains(keycode);
}

/// <summary>
/// type of keypress
/// </summary>
public enum KeypressType
{
    /// <summary>
    /// the key is currently pressed
    /// </summary>
    pressed,
    /// <summary>
    /// the key was just released; just stopped being pressed
    /// </summary>
    released,
    /// <summary>
    /// the key just started being pressed
    /// </summary>
    justPressed,
    /// <summary>
    /// the key is not being pressed
    /// </summary>
    inactive,
}