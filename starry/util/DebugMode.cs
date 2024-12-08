using System;
using System.Diagnostics;
using System.Threading.Tasks;
namespace starry;

/// <summary>
/// debug mode :D
/// </summary>
public static class DebugMode {
    static Font font = new();
    static Sprite white = new();

    public static async Task create()
    {
        font = await Starry.load<Font>("font/pixel-unicode.ttf");
        white = await Starry.load<Sprite>("white.png");
    }

    public static async Task update()
    {
        await Task.Run(() => {
            var p = Process.GetCurrentProcess();

            // i know
            Graphics.drawSprite(white, (0, 0, Starry.settings.renderSize.x, Starry.settings.renderSize.y), (0, 0), 0, (0, 0, 0, 80));

            // majestic!
            // i can't be bothered to make the lines not comically long
            // TODO add tilemap coordinates and delta time
            // TODO make it only show up with f3
            Graphics.drawText($"Stellarthing {Starry.settings.gameVersion.asVersion()}", font, (0, 0), color.white);
            Graphics.drawText($"Starry {Starry.starryVersion.asVersion()}", font, (0, 16), color.white);
            Graphics.drawText($"Running {Environment.OSVersion.VersionString}", font, (0, 32), color.white);
            Graphics.drawText($"Memory (.NET heap): {GC.GetTotalMemory(false) / 1024f / 1024f} MB", font, (0, 64), color.white);
            Graphics.drawText($"Memory (physical): {p.WorkingSet64 / 1024f / 1024f} MB", font, (0, 80), color.white);
            Graphics.drawText($"Rendering queue: {Graphics.actions.Count} events", font, (0, 96), color.white);
        });
    }
}