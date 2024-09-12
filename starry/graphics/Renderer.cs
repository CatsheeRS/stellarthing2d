using System;
using System.Collections.Generic;
using System.IO;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering. this works by having to submit rendering instructions, before actually rendering anything, which is great to make 2d less fucky
/// </summary>
public static class Renderer {
    internal static Camera3D camera3d;
    static RenderTexture2D target3d;
    static RenderTexture2D target2d;
    static decimal scaleFactor = 1;
    static decimal centerOffset = 0;
    static decimal scrw = 0;
    static decimal scrh = 0;
    static Shader lightshader;

    internal static void create()
    {
        target3d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        target2d = Raylib.LoadRenderTexture(settings.renderSize.x, settings.renderSize.y);
        camera3d = new() {
            Position = new System.Numerics.Vector3(-5.0f, 2.0f, -5.0f),
            Target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
            Up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f),
            FovY = 45.0f,
            Projection = CameraProjection.Perspective,
        };

        // get scaling factor, the screen width and height are decimal so it doesn't fuck up the calculation with ints,
        // and we use decimals instead of double so pixels aren't 0.001 pixels bigger than they should be
        scrw = Raylib.GetScreenWidth();
        scrh = Raylib.GetScreenHeight();
        scaleFactor = scrh / settings.renderSize.y;
        centerOffset = (scrw - (settings.renderSize.x * (scrh / settings.renderSize.y))) / 2m;

        // i predict this create function will eventually become comically large
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
            if (isDebug()) Raylib.DrawFPS(0, 0);
        Raylib.EndTextureMode();
        
        Raylib.BeginTextureMode(target3d);
            Raylib.ClearBackground(Color.Black);
            Raylib.BeginMode3D(camera3d);
                if (isDebug()) Raylib.DrawGrid(100, 1);
    }

    internal static void composite()
    {
            Raylib.EndMode3D();
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