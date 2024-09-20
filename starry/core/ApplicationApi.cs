using System;
using System.Numerics;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static partial class Application {
    /// <summary>
    /// time in seconds since the engine has started up
    /// </summary>
    public static double getTime() => Raylib.GetTime();

    /// <summary>
    /// gets the virtual mouse position, in the game's render size.
    /// </summary>
    public static vec2 getMousePosition()
    {
        Vector2 mouse = Raylib.GetMousePosition();
        vec2 virt = vec2();
        virt.x = (mouse.X - ((Raylib.GetScreenWidth() - (settings.renderSize.x * Renderer.scaleFactor)) * 0.5))
            / Renderer.scaleFactor;
        virt.y = (mouse.Y - ((Raylib.GetScreenHeight() - (settings.renderSize.y * Renderer.scaleFactor)) * 0.5))
            / Renderer.scaleFactor;
        virt = vec2(Math.Clamp(virt.x, 0, settings.renderSize.x), Math.Clamp(virt.y, 0, settings.renderSize.y));
        return virt;
    }
}