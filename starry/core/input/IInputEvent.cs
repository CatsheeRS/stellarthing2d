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
    /// a mouse button was pressed
    /// </summary>
    mouseButton,
}