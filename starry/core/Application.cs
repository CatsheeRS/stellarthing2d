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

        prevtime = getTime();

        // this is where the game starts running
        settings.startup();

        // main loop
        while (!Raylib.WindowShouldClose()) {
            // get delta time :D
            double delta = getTime() - prevtime;
            prevtime = getTime();

            // render stuff and update entities since that's when entities render stuff
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("hi mom", 12, 12, 20, Color.White);
            World.updateEntities();

            Raylib.EndDrawing();
        }

        // clean up and close and stuff
        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Raylib.CloseWindow();
    }
}