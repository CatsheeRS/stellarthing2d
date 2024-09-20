using System;
using Raylib_cs;
using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static partial class Renderer {
    static RenderTexture2D targetWorld;
    static RenderTexture2D targetUi;
    internal static float scaleFactor = 1;
    static int scrw = 0;
    static int scrh = 0;

    internal static void create()
    {
        targetWorld = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        targetUi = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);

        // get scaling factor, the screen width and height are decimal so it doesn't fuck up the calculation with ints,
        // and we use decimals instead of double so pixels aren't 0.001 pixels bigger than they should be
        scrw = Raylib.GetScreenWidth();
        scrh = Raylib.GetScreenHeight();        
        // we put (float) so it doesn't do integer scaling, this isn't a pixel art game
        scaleFactor = Math.Min((float)scrw / settings.renderSize.x, (float)scrh / settings.renderSize.y);
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
            Raylib.BeginMode2D(Tilemap.rlcam);
                Raylib.ClearBackground(Color.Blank);
    }

    /// <summary>
    /// finishes rendering :)
    /// </summary>
    internal static void composite()
    {
            Raylib.EndMode2D();
        Raylib.EndTextureMode();

        // actually draw stuff
        Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            // help
            Raylib.DrawTexturePro(
                targetWorld.Texture,
                new Rectangle(0f, 0f, targetWorld.Texture.Width, -targetWorld.Texture.Height),
                new Rectangle((Raylib.GetScreenWidth() - settings.renderSize.x * scaleFactor) * 0.5f,
                    (Raylib.GetScreenHeight() - settings.renderSize.y * scaleFactor) * 0.5f, settings.renderSize.x
                    * scaleFactor, settings.renderSize.y * scaleFactor),
                new Vector2(0, 0),
                0f,
                Color.White
            );
            Raylib.DrawTexturePro(
                targetUi.Texture,
                new Rectangle(0f, 0f, targetUi.Texture.Width, -targetUi.Texture.Height),
                new Rectangle((Raylib.GetScreenWidth() - settings.renderSize.x * scaleFactor) * 0.5f,
                    (Raylib.GetScreenHeight() - settings.renderSize.y * scaleFactor) * 0.5f, settings.renderSize.x
                    * scaleFactor, settings.renderSize.y * scaleFactor),
                new Vector2(0, 0),
                0f,
                Color.White
            );
        Raylib.EndDrawing();
    }

    internal static void cleanup()
    {
        Raylib.UnloadRenderTexture(targetWorld);
        Raylib.UnloadRenderTexture(targetUi);
    }
}