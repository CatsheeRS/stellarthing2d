using System;
using Silk.NET.SDL;

namespace frambos.core;

/// <summary>
/// handles communication between the renderer and silk.net/the OS
/// </summary>
public static unsafe class MainLoop {
    internal static Sdl sdl { get; set; }
    internal static Window* window { get; set; }
    internal static Renderer* render { get; set; }

    /// <summary>
    /// startups the engine
    /// </summary>
    public static unsafe void setup(string[] args, Action start_game)
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

        // setup sdl
        sdl = Sdl.GetApi();
        if (sdl.Init(Sdl.InitVideo) < 0) {
            Frambos.log("SDL ERROR: ", new string((char*)sdl.GetError()));
            return;
        }

        Window* window = sdl.CreateWindow("Space Game", 0, 0, 1280, 720, 0);
        if (window == null) {
            Frambos.log("SDL ERROR: ", new string((char*)sdl.GetError()));
            return;
        }
        sdl.SetWindowFullscreen(window, (uint)WindowFlags.FullscreenDesktop);

        render = sdl.CreateRenderer(window, -1, 0);
        if (render == null) {
            Frambos.log("SDL ERROR: ", new string((char*)sdl.GetError()));
            return;
        }

        sdl.SetRenderDrawColor(render, 0, 0, 0, 1);
        Frambos.log("done setting up window");

        double prev_time = sdl.GetTicks64() / 1000d;
        double delta = 0;
        
        // main loop :D
        bool quit = false;
        Event e = new();
        while (!quit) {
            while (sdl.PollEvent(ref e) != 0) {
                if (e.Type == (uint)EventType.Quit) {
                    Frambos.log("close request");
                    quit = true;
                    break;
                }
            }

            double cur_time = sdl.GetTicks64() / 1000d;
            delta = cur_time - prev_time;
            prev_time = cur_time;

            Frambos.log($"updating, delta time = {delta:F10}");

            // rendering (:
            Frambos.log("rendering");
            sdl.RenderClear(render);
            // rendering goes here lmao
            sdl.RenderPresent(render);
        }

        sdl.DestroyRenderer(render);
        sdl.DestroyWindow(window);
        sdl.Quit();

        Frambos.log("game finished running");
    }
}
