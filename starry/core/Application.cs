using System;
using System.Diagnostics;
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
    //static bool isfullscreen = true;

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        // setup stuff
        Platform.createWindow(new WindowSettings {
            title = $"{settings.gameName} {settings.gameVersion}",
            size = vec2i(2000, 2000), // i fucking hate sdl
            renderSize = settings.renderSize,
            type = WindowType.fullscreenBorderless,
            targetFps = 60,
        });
        windowCreated = true;

        // more setup
        prevtime = Platform.getTime() / 1000d;
        log("Engine finished startup");

        // this is where the game starts running
        settings.startup();

        // main loop
        while (!Platform.shouldClose()) {
            Platform.startUpdate();

            // get delta time :D
            delta = Platform.getTime() - prevtime;
            prevtime = Platform.getTime();

            // set fullscreen
            // if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
            //     if (isfullscreen) Raylib.SetWindowState(ConfigFlags.ResizableWindow);
            //     else Raylib.SetWindowState(ConfigFlags.FullscreenMode);
            // }

            if (isDebug()) {
                if (Input.isKeyJustPressed(Key.f8)) break;
            }

            // yugfvyi8gbyugbi
            World.updateEntities();
            Tilemap.update();
            DebugMode.update();

            Platform.endUpdate();
        }

        // clean up and close and stuff
        log("Ending engine");
        onClose?.Invoke(typeof(Application), EventArgs.Empty);
        Assets.cleanup();
        Platform.cleanup();
        log("🛑 ITS JOEVER");
    }
}