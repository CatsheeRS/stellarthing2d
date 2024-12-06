using System;
using SkiaSharp;
namespace starry;

/// <summary>
/// the renderer. this is just skia lamo
/// </summary>
public static partial class Graphics {
    internal static SKCanvas? canvas;
    internal static SKSurface? surface;
    internal static GRContext? grContext;
    internal static SKPaint? skpaint;
    /// <summary>
    /// the scale factor for the thingy
    /// </summary>
    public static int scale { get; private set; } = 0;
    /// <summary>
    /// offset because not every screen is gonna scale exactly perfectly
    /// </summary>
    public static vec2i offset { get; private set; } = (0, 0);

    public static void create()
    {
        // shut up
        if (Window.glfw == null) return;

        // setup fucking skia fucking sharp
        var glInterface = GRGlInterface.CreateOpenGl(Window.glfw.GetProcAddress);
        grContext = GRContext.CreateGl(glInterface);

        vec2i winsize = Window.getSize();
        GRGlFramebufferInfo frameBufferInfo = new(0, SKColorType.Rgba8888.ToGlSizedFormat());
        GRBackendRenderTarget renderTarget = new((int)winsize.x, (int)winsize.y, 0, 8,
            frameBufferInfo);
        
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.TopLeft,
            SKColorType.Rgba8888);

        canvas = surface.Canvas;

        // sick pain(t) stuff
        skpaint = new SKPaint() {
            Color = SKColors.White,
            IsAntialias = false, // this is a pixel art game
        };

        // calculate the scale stuff
        scale = (int)Math.Min(winsize.x / Starry.settings.renderSize.x, winsize.y /
            Starry.settings.renderSize.y);
        offset = ((winsize - (Starry.settings.renderSize * (vec2)(scale, scale))) *
            (0.5, 0.5)).round();

        Starry.log("Skia has loaded");
    }

    internal static void cleanup()
    {
        surface?.Dispose();
        grContext?.Dispose();
        Starry.log("Skia has been annihilated");
    }
}