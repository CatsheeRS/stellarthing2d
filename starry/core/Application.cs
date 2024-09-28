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
    static bool isfullscreen = true;

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        // setup stuff
        Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), settings.gameName);
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
        Tilemap.create();
        prevtime = getTime();
        log("Engine finished startup");

        // this is where the game starts running
        settings.startup();

        // main loop
        while (!Raylib.WindowShouldClose()) {
            // get delta time :D
            delta = getTime() - prevtime;
            prevtime = getTime();

            // set fullscreen
            if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
                if (isfullscreen) Raylib.SetWindowState(ConfigFlags.ResizableWindow);
                else Raylib.SetWindowState(ConfigFlags.FullscreenMode);
            }

            // render stuff and update entities since that's when entities render stuff
            // the renderer is called by the world since it has to switch between 2d and 3d and stuff
            World.updateEntities();
            Tilemap.update();
            Renderer.composite();
        }

        // clean up and close and stuff
        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Assets.cleanup();
        Renderer.cleanup();
        Raylib.CloseWindow();
    }
}