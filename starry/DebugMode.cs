using System;
using static starry.Starry;
namespace starry;

/// <summary>
/// handles debug mode stuff :D
/// </summary>
public static class DebugMode {
    static bool ondebug = false;

    internal static void update()
    {
        if (Input.isKeyJustPressed(Key.f3)) ondebug = !ondebug;
        if (!ondebug) return;

        Platform.drawTexture(load<Sprite>("white.png"), vec2i(), settings.renderSize, color(0, 0, 0, 50));

        string text =
            $@"Stellarthing {settings.gameVersion}
            {Platform.getFps()} FPS
            Running {Environment.OSVersion.VersionString}
            Tilemap: x, y: {Camera.target}; world ""{Tilemap.world}""; layer {Tilemap.layer}
            Memory: {GC.GetTotalMemory(false) / 1_049_000f} MB";
        
        Platform.drawText(text, vec2i(), vec2i(1, 1), color.white);
    }
}