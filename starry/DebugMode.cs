using System;
using static starry.Starry;
namespace starry;

/// <summary>
/// handles debug mode stuff :D
/// </summary>
public static class DebugMode {
    internal static void update()
    {
        if (!Input.isKeyPressed(Key.f3)) return;

        Platform.drawTexture(load<Sprite>("white.png"), vec2i(), settings.renderSize, color(0, 0, 0, 50));

        // majestic!
        string text =
$@"Stellarthing {settings.gameVersion}
Delta time: {Application.delta:F6}s
Running {Environment.OSVersion.VersionString}
Tilemap: x, y: {Camera.target}; world ""{Tilemap.world}""; layer {Tilemap.layer}
Memory: {GC.GetTotalMemory(false) / 1_049_000} MB";
        
        Platform.drawText(text, vec2i(), vec2i(1, 1), color.white);
    }
}