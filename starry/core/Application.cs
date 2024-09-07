using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static class Application {
    /// <summary>
    /// the screen size
    /// </summary>
    public static Vector2i screenSize { get; private set; } = vec2i();
    /// <summary>
    /// current delta time
    /// </summary>
    public static double delta { get; set; } = 0;
    static double prevtime;

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        Raylib.InitWindow(800, 480, "Hello World");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}