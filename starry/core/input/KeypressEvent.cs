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
    public Key key { get; set; }
    public KeypressState type { get; set; }
    public bool isKeymap(string key) => settings.keymap[key].Contains(this.key);
}

/// <summary>
/// type of keypress
/// </summary>
public enum KeypressState
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
    /// the key is being repeated. don't use this for checking if a key is being held down, as this is intended for inputting text and behaves like it would in a text editor, where the repeat interval is configurable and there's a bit of delay before starting to repeat.
    /// </summary>
    //textRepeat,
    /// <summary>
    /// the key is not being pressed
    /// </summary>
    inactive,
}