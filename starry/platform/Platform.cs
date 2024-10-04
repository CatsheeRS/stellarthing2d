using System;
using SDL2;
using static starry.Starry;

namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it
/// </summary>
public static class Platform
{
    static WindowSettings settings;
    static nint window;
    static bool running = true;
    static ulong startTicks = 0;
    static double fps = 0;
    static nint sdlRender;
    public static event EventHandler? onInput;

    /// <summary>
    /// creates the window and stuff
    /// </summary>
    /// <param name="settings"></param>
    public static void createWindow(WindowSettings settings)
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0) {
            log("FATAL ERROR: SDL couldn't initialize.");
            return;
        }

        // make flags since it's kinda fucky
        SDL.SDL_WindowFlags flags = 0;
        flags |= settings.type switch {
            WindowType.windowed => 0,
            WindowType.fullscreen => SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            WindowType.borderless => SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            WindowType.fullscreenBorderless => SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN |
                                               SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            _ => throw new Exception("you shouldn't do that with the WindowType enum"),
        };

        window = SDL.SDL_CreateWindow(settings.title, 100, 100, settings.size.x, settings.size.y, flags);
        if (window == 0) {
            log("FATAL ERROR: Couldn't create window");
            return;
        }

        sdlRender = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        if (sdlRender == 0) {
            log("FATAL ERROR: Couldn't create renderer");
            return;
        }

        Platform.settings = settings;
    }

    /// <summary>
    /// returns the time that has elapsed since the window was created, in milliseconds
    /// </summary>
    public static ulong getTime()
    {
        return SDL.SDL_GetTicks64();
    }

    /// <summary>
    /// it handles events :D
    /// </summary>
    public static void handleEvents()
    {
        // TODO
    }

    // if true, the window requested to be close
    public static bool shouldClose()
    {
        // TODO
    }

    /// <summary>
    /// you should run this at the start of your main loop for things to work
    /// </summary>
    public static void startUpdate()
    {
        startTicks = SDL.SDL_GetTicks64();
    }

    /// <summary>
    /// you should run this at the end of your main loop for things to work
    /// </summary>
    public static void endUpdate()
    {
        ulong endTicks = SDL.SDL_GetTicks64();
        fps = 1 / ((endTicks - startTicks) / 1000f);
    }

    /// <summary>
    /// the current fps of the game
    /// </summary>
    public static double getFps()
    {
        return fps;
    }

    /// <summary>
    /// cleans up the internal stuff
    /// </summary>
    public static void cleanup()
    {
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_DestroyRenderer(sdlRender);
        SDL.SDL_Quit();
    }
}