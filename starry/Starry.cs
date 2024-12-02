using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raylib_cs;
namespace starry;

public class Starry {
    /// <summary>
    /// starry settings
    /// </summary>
    public static StarrySettings settings { get; set; }
    /// <summary>
    /// the engine version (semantic versioning)
    /// </summary>
    public static vec3i starryVersion => (2, 0, 1);

    public static async Task create(StarrySettings settings)
    {
        Starry.settings = settings;
        // if i call it "title" it becomes 122 characters so i have an entire line that's just ";
        string el = $"{settings.gameName} v{settings.gameVersion.x}.{settings.gameVersion.y}.{settings.gameVersion.z}";
        // the size doesn't matter once you make it fullscreen
        Window.create(el, settings.windowSize);
        Window.setFullscreen(settings.fullscreen);

        // fccking kmodules
        await Graphics.create();

        while (!Window.isClosing()) {
            Graphics.clear(color.white);
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
            Graphics.endDrawing();
        }

        // fccking kmodules
        await Graphics.cleanup();
        Window.cleanup();
    }
}