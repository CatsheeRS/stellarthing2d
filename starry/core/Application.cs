using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static partial class Application {
    /// <summary>
    /// current delta time
    /// </summary>
    public static double delta { get; set; } = 0;
    static double prevtime;

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        // setup stuff
        Raylib.InitWindow(1280, 720, "Hello World");
        Raylib.SetWindowState(ConfigFlags.FullscreenMode);
        #if DEBUG
        Raylib.SetExitKey(KeyboardKey.F8);
        #else
        Raylib.SetExitKey(KeyboardKey.Null);
        #endif

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Raylib.CloseWindow();
    }
}