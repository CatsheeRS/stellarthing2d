using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static class Application {
    /// <summary>
    /// current delta time
    /// </summary>
    public static double delta { get; set; } = 0;
    /// <summary>
    /// if true, raylib has been setup
    /// </summary>
    public static bool windowCreated { get; set; } = false;
    static double prevtime;
    static bool isfullscreen = true;

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        // setup stuff
        Platform.createWindow(new WindowSettings {
            title = $"{settings.gameName} {settings.gameVersion}",
            size = vec2i(1, 1), // this doesn't matter when the window is fullscreen borderless
            renderSize = settings.renderSize,
            type = WindowType.fullscreenBorderless,
            targetFps = 60,
        });
        windowCreated = true;

        // more setup
        Renderer.create();
        Tilemap.create();
        DebugMode.create();
        prevtime = Platform.getTime() / 1000d;
        log("Engine finished startup");

        // this is where the game starts running
        settings.startup();

        // main loop
        while (!Platform.shouldClose()) {
            // get delta time :D
            delta = Platform.getTime() - prevtime;
            prevtime = Platform.getTime();

            // set fullscreen
            // if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
            //     if (isfullscreen) Raylib.SetWindowState(ConfigFlags.ResizableWindow);
            //     else Raylib.SetWindowState(ConfigFlags.FullscreenMode);
            // }

            // TODO: remake the f8 to exit on debug mode thing

            // render stuff and update entities since that's when entities render stuff
            // the renderer is called by the world since it has to switch between 2d and 3d and stuff
            World.updateEntities();
            Tilemap.update();
            Renderer.composite();
        }

        // clean up and close and stuff
        log("Ending engine");
        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Assets.cleanup();
        Renderer.cleanup();
        Platform.cleanup();
    }
}