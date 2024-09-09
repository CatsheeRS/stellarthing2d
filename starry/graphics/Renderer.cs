using System;
using System.Collections.Generic;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering. this works by having to submit rendering instructions, before actually rendering anything, which is great to make 2d less fucky
/// </summary>
public static class Renderer {
    static bool rendering3d = true;
    static RenderTexture2D target3d;
    static RenderTexture2D target2d;

    internal static void create()
    {
        target3d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        target2d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
    }
}