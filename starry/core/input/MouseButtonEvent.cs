using System;
using System.Linq;
using static starry.Starry;

namespace starry;

/// <summary>
/// trigged when a key is pressed
/// </summary>
public class MouseButtonEvent : IInputEvent {
    public InputType getType() => InputType.mouseButton;

    public MouseButton button { get; set; }
    public MouseButtonState state { get; set; }
}

/// <summary>
/// type of mouse button pressed
/// </summary>
public enum MouseButtonState {
    /// <summary>
    /// the button just started being pressed
    /// </summary>
    justPressed,
    /// <summary>
    /// the button was just released; just stopped being pressed
    /// </summary>
    released,
}

/// <summary>
/// mouse button
/// </summary>
public enum MouseButton {
    left, middle, right
}