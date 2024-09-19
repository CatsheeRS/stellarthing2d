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
        Raylib.InitWindow(settings.renderSize.x, settings.renderSize.y, settings.gameName);
        Raylib.SetWindowState(ConfigFlags.FullscreenMode);
        if (isDebug()) {
            Raylib.SetExitKey(KeyboardKey.F8);
        }
        else {
            Raylib.SetExitKey(KeyboardKey.Null);
        }

        // more setup
        Raylib.SetTargetFPS(60);
        Renderer.create();

        prevtime = getTime();

        // this is where the game starts running
        settings.startup();

        // main loop
        while (!Raylib.WindowShouldClose()) {
            // get delta time :D
            double delta = getTime() - prevtime;
            prevtime = getTime();

            // render stuff and update entities since that's when entities render stuff
            Raylib.DrawText("hi mom", 12, 12, 20, Color.White);
            // the renderer is called by the world since it has to switch between 2d and 3d and stuff
            World.updateEntities();
            Tilemap.update();
            Renderer.composite();
        }

        // clean up and close and stuff
        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Assets.cleanup();
        Raylib.CloseWindow();
    }
}