using System;
using frambos.graphics;
using frambos.util;
using Silk.NET.GLFW;
using Silk.NET.Input;
using Silk.NET.OpenGL;

namespace frambos.core;

/// <summary>
/// handles communication between the renderer and silk.net/the OS
/// </summary>
public static class MainLoop {
    internal static Glfw glfw { get; set; }

    /// <summary>
    /// startups the engine
    /// </summary>
    public static void setup(string[] args)
    {
        // unrequested help command
        Console.WriteLine("Options:");
        Console.WriteLine("  -v, --verbose: shows error messages, helpful for troubleshooting");

        // first check arguments for the engine
        foreach (var arg in args) {
            if (arg == "-v" || arg == "--verbose") {
                Frambos.verbose_mode = true;
            }
        }

        Frambos.log("starting up engine");
        Frambos.log("setting up window");

        setup_window();

        Frambos.log("game finished running");
    }

    static unsafe void setup_window()
    {
        glfw = Glfw.GetApi();
        glfw.SetErrorCallback(opengl_error);
        glfw.Init();

        // figure out fullscreen
        Vector2 resolution;
        Monitor* mtr = glfw.GetPrimaryMonitor();
        VideoMode* mode = glfw.GetVideoMode(mtr);   
        resolution = new Vector2(mode->Height, mode->Width);

        // make the window
        WindowHandle* window = glfw.CreateWindow((int)resolution.x, (int)resolution.y, "Space Game™", mtr, null);
        if (window == null) {
            Frambos.log("critical error: window or context creation failed");
            glfw.Terminate();
            return;
        }
        glfw.MakeContextCurrent(window);

        // setup delta time
        double prev_time = glfw.GetTime();
        double delta = 0;
        
        // non-window loading :D
        load(GL.GetApi(glfw.Context));

        // main loop :)
        while (!glfw.WindowShouldClose(window)) {
            double cur_time = glfw.GetTime();
            delta = cur_time - prev_time;
            prev_time = cur_time;

            update(delta);
            render(window);
        }

        // close stuff
        glfw.DestroyWindow(window);
        glfw.Terminate();
    }

    static void load(GL gl)
    {
        Frambos.log("loading");
        Renderer.setup(gl);
    }

    static void update(double delta)
    {
        Frambos.log("updating, delta = ", delta);
    }

    static unsafe void render(WindowHandle* window)
    {
        glfw.SwapBuffers(window);
        glfw.PollEvents();
    }

    static void opengl_error(Silk.NET.GLFW.ErrorCode error, string description)
    {
        Frambos.log("OPENGL ERROR: ", description);
    }
}
