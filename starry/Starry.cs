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

        settings.startup();

        while (!Window.isClosing()) {
            Graphics.clear(color.black);
            Graphics.drawText("Rewolucja przemysłowa i jej konsekwencje okazały się katastrofą dla rodzaju ludzkiego.",
                Graphics.defaultFont, (16, 16), color.purple, 16);
            Graphics.endDrawing();
        }

        // fccking kmodules
        await Graphics.cleanup();
        await Assets.cleanup();
        Window.cleanup();
    }

    /// <summary>
    /// loads the assets and then puts it in a handsome dictionary of stuff so its blazingly fast or smth idfk this is just Assets.load<T> lmao
    /// </summary>
    public static async Task<T> load<T>(string path) where T: IAsset, new() => await Assets.load<T>(path);
}