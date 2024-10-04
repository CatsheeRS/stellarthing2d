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

    /// <summary>
    /// creates the window and stuff
    /// </summary>
    /// <param name="settings"></param>
    public static void createWindow(WindowSettings settings)
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0) {
            log("FATAL ERROR: SDL couldn't initialize.");
        }

        // make flags since it's kinda fucky
        SDL.SDL_WindowFlags flags = 0;
        flags |= settings.type switch {
            WindowType.windowed => 0,
            WindowType.fullscreen => SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            WindowType.hidden => SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN,
            WindowType.borderless => SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            WindowType.fullscreenBorderless => SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN |
                                               SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            _ => throw new Exception("you shouldn't do that with the WindowType enum"),
        };
        SDL.SDL_CreateWindow(settings.title, 100, 100, settings.size.x, settings.size.y, flags);
        Platform.settings = settings;
    }
}