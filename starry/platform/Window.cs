using System;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace starry;

/// <summary>
/// the game window. static since i don't think games need several of those
/// </summary>
public static unsafe class Window {
    internal static Glfw? glfw;
    internal static WindowHandle* window;
    internal static bool fullscreen = false;

    /// <summary>
    /// creates the window :D
    /// </summary>
    public static unsafe void create(string title, vec2i size)
    {
        // first we need glfw
        glfw = Glfw.GetApi();
        if (!glfw.Init()) {
            // crash because glfw is quite important
            throw new Exception("Couldn't initialize GLFW");
        }

        // hints :D
        glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
        glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
        glfw.WindowHint(WindowHintInt.RefreshRate, Starry.settings.frameRate);

        glfw.SetErrorCallback((error, description) => {
            Starry.log($"OPENGL ERROR: {error}: {description}");
        });

        Starry.log("GLFW has been initialized");

        // make the infamous window
        window = glfw.CreateWindow((int)Starry.settings.renderSize.x,
            (int)Starry.settings.renderSize.y, title, null, null);
        if (window == null) {
            glfw.Terminate();
            throw new Exception("Couldn't create a window");
        }
        glfw.MakeContextCurrent(window);
        Starry.log("Created window");

        // this setups up opengl
        Graphics.create();
    }

    /// <summary>
    /// if true, the window is gonna be fullscreen
    /// </summary>
    public static void setFullscreen(bool fullscreen)
    {
        if (glfw == null) return;
        Window.fullscreen = fullscreen;

        if (fullscreen) {
            Monitor* monitor = glfw.GetPrimaryMonitor();
            if (monitor == null) return;
            VideoMode* mode = glfw.GetVideoMode(monitor);
            glfw.SetWindowMonitor(window, monitor, 0, 0, mode->Width, mode->Height,
                mode->RefreshRate);
        }
        else glfw.SetWindowMonitor(window, null, 40, 40, (int)Starry.settings.renderSize.x,
            (int)Starry.settings.renderSize.y, Starry.settings.frameRate);
    }

    /// <summary>
    /// if true, the window is currently fullscreen
    /// </summary>
    public static bool isFullscreen() => fullscreen;

    /// <summary>
    /// if true the window is closing. convenient for making a main loop
    /// </summary>
    public static bool isClosing() => glfw?.WindowShouldClose(window) ?? false;

    /// <summary>
    /// run at the end of the thing
    /// </summary>
    public static void cleanup()
    {
        if (glfw == null) return;

        glfw.DestroyWindow(window);
        glfw.Terminate();
    }

    /// <summary>
    /// the size of the window
    /// </summary>
    public static vec2i getSize()
    {
        if (glfw == null) return (0, 0);
        glfw.GetWindowSize(window, out int width, out int height);
        return (width, height);
    }
}