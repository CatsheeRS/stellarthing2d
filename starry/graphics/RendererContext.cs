using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static partial class Renderer {
    static RenderTexture2D target3d;
    static RenderTexture2D target2d;
    static decimal scaleFactor = 1;
    static decimal centerOffset = 0;
    static decimal scrw = 0;
    static decimal scrh = 0;

    internal static void create()
    {
        target3d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        target2d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);

        // get scaling factor, the screen width and height are decimal so it doesn't fuck up the calculation with ints,
        // and we use decimals instead of double so pixels aren't 0.001 pixels bigger than they should be
        scrw = Raylib.GetScreenWidth();
        scrh = Raylib.GetScreenHeight();
        scaleFactor = scrh / settings.renderSize.y;
        centerOffset = (scrw - (settings.renderSize.x * (scrh / settings.renderSize.y))) / 2m;

        // do stuff
        setupProjectionMatrix(0.1f, 1000.0f, 90.0f);
    }

    /// <summary>
    /// switches to rendering 2d. ending the thing is handled when the game switches to 3d
    /// </summary>
    internal static void render2d()
    {
        Raylib.BeginTextureMode(target2d);
            Raylib.ClearBackground(Color.Blank);
    }

    /// <summary>
    /// switches to rendering 3d and finishes 2d stuff. 3d rendering is finished in the composite function
    /// </summary>
    internal static void render3d()
    {
            //if (isDebug()) Raylib.DrawFPS(0, 0);
        Raylib.EndTextureMode();
        
        Raylib.BeginTextureMode(target3d);
            Raylib.ClearBackground(Color.Black);
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

            Raylib.DrawTextureRec(target3d.Texture, new Rectangle((float)centerOffset, 0,
                (float)(settings.renderSize.x * scaleFactor), -(float)(settings.renderSize.y * scaleFactor)),
                new System.Numerics.Vector2(0, 0), Color.White);
            
            Raylib.DrawTextureRec(target2d.Texture, new Rectangle((float)centerOffset, 0,
                (float)(settings.renderSize.x * scaleFactor), -(float)(settings.renderSize.y * scaleFactor)),
                new System.Numerics.Vector2(0, 0), Color.White);
        Raylib.EndDrawing();
    }
}