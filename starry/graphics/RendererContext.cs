using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static partial class Renderer {
    static RenderTexture2D targetWorld;
    static RenderTexture2D targetUi;
    static decimal scaleFactor = 1;
    static decimal centerOffset = 0;
    static decimal scrw = 0;
    static decimal scrh = 0;

    internal static void create()
    {
        targetWorld = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        targetUi = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);

        // get scaling factor, the screen width and height are decimal so it doesn't fuck up the calculation with ints,
        // and we use decimals instead of double so pixels aren't 0.001 pixels bigger than they should be
        scrw = Raylib.GetScreenWidth();
        scrh = Raylib.GetScreenHeight();
        scaleFactor = scrh / settings.renderSize.y;
        centerOffset = (scrw - (settings.renderSize.x * (scrh / settings.renderSize.y))) / 2m;
    }

    /// <summary>
    /// switches to rendering the ui. ending the thing is handled when the game switches to the game world
    /// </summary>
    internal static void renderUi()
    {
        Raylib.BeginTextureMode(targetUi);
            Raylib.ClearBackground(Color.Blank);
    }

    /// <summary>
    /// switches to rendering the world and finishes ui stuff. world rendering is finished in the composite function
    /// </summary>
    internal static void renderWorld()
    {
            if (isDebug()) Raylib.DrawFPS(0, 0);
        Raylib.EndTextureMode();
        
        Raylib.BeginTextureMode(targetWorld);
            Raylib.ClearBackground(Color.Blank);
    }

    /// <summary>
    /// finishes rendering :)
    /// </summary>
    internal static void composite()
    {
        Raylib.EndTextureMode();

        // actually draw stuff
        Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawTextureRec(targetWorld.Texture, new Rectangle((float)centerOffset, 0,
                (float)(settings.renderSize.x * scaleFactor), -(float)(settings.renderSize.y * scaleFactor)),
                new System.Numerics.Vector2(0, 0), Color.White);
            
            Raylib.DrawTextureRec(targetUi.Texture, new Rectangle((float)centerOffset, 0,
                (float)(settings.renderSize.x * scaleFactor), -(float)(settings.renderSize.y * scaleFactor)),
                new System.Numerics.Vector2(0, 0), Color.White);
        Raylib.EndDrawing();
    }
}