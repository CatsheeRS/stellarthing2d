using System;
using frambos.graphics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace frambos.core;

/// <summary>
/// handles communication between the renderer and silk.net/the OS
/// </summary>
public class MainLoop {
    /// <summary>
    /// the silk.net object representing the window
    /// </summary>
    public static IWindow window { get; set; }

    /// <summary>
    /// startups the engine
    /// </summary>
    public MainLoop(string[] args)
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

    void setup_window()
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "Space Game",
        };
        window = Window.Create(options);
        window.Load += Renderer.on_load;
        window.Update += Renderer.on_update;
        window.Render += Renderer.on_render;
        window.Run();
    }
}
