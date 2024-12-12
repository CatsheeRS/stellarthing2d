using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Silk.NET.GLFW;

namespace starry;

/// <summary>
/// debug mode :D
/// </summary>
public static class DebugMode {
    static Font font = new();
    static Sprite white = new();

    static bool shown = false;
    public static async Task create()
    {
        font = await Starry.load<Font>("font/pixel-unicode.ttf");
        white = await Starry.load<Sprite>("white.png");
        
        Window.keyPress += (sender, args) =>
        {
            if (args.Key == Keys.F3) shown = !shown;
        };
    }

    public static async Task update()
    {
        await Task.Run(() =>
        {
            if (!shown) return;
            
            var p = Process.GetCurrentProcess();

            // i know
            Graphics.drawSprite(white, (0, 0, Starry.settings.renderSize.x, Starry.settings.renderSize.y), (0, 0), 0, (0, 0, 0, 80));

            // majestic!
            // i can't be bothered to make the lines not comically long
            // TODO make it only show up with f3
            Graphics.drawText($"Stellarthing {Starry.settings.gameVersion.asVersion()}", font, (0, 0), color.white);
            Graphics.drawText($"Starry {Starry.starryVersion.asVersion()}", font, (0, 16), color.white);
            Graphics.drawText($"Running {Environment.OSVersion.VersionString}", font, (0, 16 * 2), color.white);
            Graphics.drawText($"{Window.fps:0} FPS", font, (0, 16 * 4), color.white);
            Graphics.drawText($"Memory (.NET heap): {GC.GetTotalMemory(false) / 1024f / 1024f:0.0000} MB", font, (0, 16 * 5), color.white);
            Graphics.drawText($"Memory (physical): {p.WorkingSet64 / 1024f / 1024f:0.0000} MB", font, (0, 16 * 6), color.white);
            Graphics.drawText($"Tilemap: @ {Tilemap.camPosition}, world {Tilemap.currentWorld}, layer {Tilemap.currentLayers[Tilemap.currentWorld]}", font, (0, 16 * 8), color.white);
        });
    }
}