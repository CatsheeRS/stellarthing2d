using System;
using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static partial class Renderer {
    static Viewport? targetWorld;
    static Viewport? targetUi;
    internal static float scaleFactor = 1;

    internal static void create()
    {
        targetWorld = new Viewport(settings.renderSize);
        targetUi = new Viewport(settings.renderSize);

        // get scaling factor, the screen width and height are decimal so it doesn't fuck up the calculation with ints
        // we convert it to a regular vec2 so it doesn't do integer scaling, this isn't a pixel art game
        vec2 ü = Platform.getScreenSize();
        scaleFactor = (float)Math.Min(ü.x / settings.renderSize.x, ü.y / settings.renderSize.y);
    }

    /// <summary>
    /// switches to rendering the ui. ending the thing is handled when the game switches to the game world
    /// </summary>
    internal static void renderUi()
    {
        targetUi?.start(color.transparent);
    }

    /// <summary>
    /// switches to rendering the world and finishes ui stuff. world rendering is finished in the composite function
    /// </summary>
    internal static void renderWorld()
    {
        targetUi?.end();
        
        targetWorld?.start(color.transparent);
    }

    /// <summary>
    /// finishes rendering :)
    /// </summary>
    internal static void composite()
    {
        targetWorld?.end();
        vec2 g = Platform.getScreenSize() - settings.renderSize * vec2(scaleFactor, scaleFactor);
        vec2 j = settings.renderSize * vec2(scaleFactor, scaleFactor);

        // actually draw stuff
        // help
        targetWorld?.render(
            vec2i(), settings.renderSize,
            vec2i((int)Math.Round(g.x), (int)Math.Round(g.y)),
            vec2i((int)Math.Round(j.x), (int)Math.Round(j.y)),
            0, vec2i(), color.white
        );
        targetUi?.render(
            vec2i(), settings.renderSize,
            vec2i((int)Math.Round(g.x), (int)Math.Round(g.y)),
            vec2i((int)Math.Round(j.x), (int)Math.Round(j.y)),
            0, vec2i(), color.white
        );

        // the debug mode is very peculiar
        DebugMode.update();
    }
}