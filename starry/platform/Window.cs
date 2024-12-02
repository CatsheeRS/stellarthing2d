using Raylib_cs;

namespace starry;

/// <summary>
/// the game window. static since i don't think games need several of those
/// </summary>
public static class Window {
    /// <summary>
    /// creates the window :D
    /// </summary>
    public static void create(string title, vec2i size)
    {
        // i hope you don't need a window of a size of 3 billion
        Raylib.InitWindow((int)size.x, (int)size.y, title);
    }

    /// <summary>
    /// if true, the window is gonna be fullscreen
    /// </summary>
    public static void setFullscreen(bool fullscreen)
    {
        if (fullscreen) Raylib.SetWindowState(ConfigFlags.FullscreenMode);
        else Raylib.SetWindowState(ConfigFlags.ResizableWindow);
    }

    /// <summary>
    /// if true, the window is currently fullscreen
    /// </summary>
    public static bool isFullscreen()
    {
        return Raylib.IsWindowState(ConfigFlags.FullscreenMode);
    }

    /// <summary>
    /// if true the window is closing. convenient for making a main loop
    /// </summary>
    public static bool isClosing() => Raylib.WindowShouldClose();

    /// <summary>
    /// run at the end of the thing
    /// </summary>
    public static void cleanup() => Raylib.CloseWindow();

    /// <summary>
    /// the size of the window (considers hidpi)
    /// </summary>
    public static vec2i getSize() => (Raylib.GetRenderWidth(), Raylib.GetRenderHeight()); // TODO use GetWindowScaleDPI when making the renderer
}