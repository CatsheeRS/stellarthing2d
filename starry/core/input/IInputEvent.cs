using static starry.Starry;

namespace starry;

// TODO: get a controller and add support for that

/// <summary>
/// base of all input events
/// </summary>
public interface IInputEvent {
    /// <summary>
    /// used for casting input events
    /// </summary>
    public InputType getType();
}

/// <summary>
/// used for casting input events
/// </summary>
public enum InputType {
    /// <summary>
    /// a key was pressed
    /// </summary>
    keypress,
    /// <summary>
    /// the mouse's position was updated
    /// </summary>
    mouse_motion,
    /// <summary>
    /// a mouse button was pressed
    /// </summary>
    mouse_button,
    /// <summary>
    /// the user has scrolled
    /// </summary>
    mouse_scroll
}