using System;
using static starry.Starry;
using static SDL2.SDL;

namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it
/// </summary>
public static class Platform
{
    static WindowSettings settings;
    static nint window;
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
        if (SDL_Init(SDL_INIT_EVERYTHING) < 0) {
            log("FATAL ERROR: SDL couldn't initialize.");
            return;
        }

        // make flags since it's kinda fucky
        SDL_WindowFlags flags = 0;
        flags |= settings.type switch {
            WindowType.windowed => 0,
            WindowType.fullscreen => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            WindowType.borderless => SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            WindowType.fullscreenBorderless => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN |
                                               SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            _ => throw new Exception("you shouldn't do that with the WindowType enum"),
        };

        window = SDL_CreateWindow(settings.title, 100, 100, settings.size.x, settings.size.y, flags);
        if (window == 0) {
            log("FATAL ERROR: Couldn't create window");
            return;
        }

        sdlRender = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
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
        return SDL_GetTicks64();
    }

    /// <summary>
    /// it handles events :D
    /// </summary>
    internal static void handleEvents(SDL_Event e)
    {
        
    }

    // if true, the window requested to be close
    public static bool shouldClose()
    {
        while (SDL_PollEvent(out SDL_Event e) != 0) {
            if (e.type == SDL_EventType.SDL_QUIT) return true;
            else handleEvents(e);
        }
        return false;
    }

    /// <summary>
    /// you should run this at the start of your main loop for things to work
    /// </summary>
    public static void startUpdate()
    {
        startTicks = SDL_GetTicks64();
    }

    /// <summary>
    /// you should run this at the end of your main loop for things to work
    /// </summary>
    public static void endUpdate()
    {
        ulong endTicks = SDL_GetTicks64();
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
        SDL_DestroyWindow(window);
        SDL_DestroyRenderer(sdlRender);
        SDL_Quit();
    }
}
