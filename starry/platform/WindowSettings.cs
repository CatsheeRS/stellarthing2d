using static starry.Starry;

namespace starry;

/// <summary>
/// window settings :D
/// </summary>
public struct WindowSettings
{
    public WindowSettings() {} 

    /// <summary>
    /// the title for the window
    /// </summary>
    public string title { get; set; } = "Starry Engine";
    /// <summary>
    /// the size of the window in pixels
    /// </summary>
    public vec2i size { get; set; } = vec2i(640, 480);
    /// <summary>
    /// the size the screen will be rendered to, if it's (0, 0) the renderer won't do any scaling
    /// </summary>
    public vec2i renderSize { get; set; } = vec2i(640, 480);
    /// <summary>
    /// the type of windowed
    /// </summary>
    public WindowType type { get; set; } = WindowType.windowed;
    /// <summary>
    /// if true, the window is maximized (technically different from fullscreen)
    /// </summary>
    public bool maximized { get; set; } = false;
    /// <summary>
    /// if true, the window is minimized
    /// </summary>
    public bool minimized { get; set; } = false;
    /// <summary>
    /// if true, the window can be resized by the user
    /// </summary>
    public bool resizable { get; set; } = false;
    /// <summary>
    /// if true, the application can use high DPI
    /// </summary>
    public bool highDpi { get; set; } = false;
    /// <summary>
    /// the target fps for the thing
    /// </summary>
    public uint targetFps { get; set; } = 60;
}

public enum WindowType
{
    /// <summary>
    /// the window is windowed :D
    /// </summary>
    windowed,
    /// <summary>
    /// the window is fullscreen
    /// </summary>
    fullscreen,
    /// <summary>
    /// the window doesn't get decorations
    /// </summary>
    borderless,
    /// <summary>
    /// both fullscreen and without decorations
    /// </summary>
    fullscreenBorderless,
}