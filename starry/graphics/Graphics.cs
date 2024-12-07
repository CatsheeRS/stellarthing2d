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
    internal static GRBackendRenderTarget? renderTarget;
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
        renderTarget = new((int)winsize.x, (int)winsize.y, 0, 8, frameBufferInfo);
        
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.TopLeft,
            SKColorType.Rgba8888);
        canvas = surface.Canvas;

        // sick pain(t) stuff
        skpaint = new SKPaint() {
            Color = SKColors.White,
            IsAntialias = false, // this is a pixel art game
        };

        calcScale(winsize);
        Window.onResize += calcScale;
        Window.onResize += resizeTarget;

        // why is it flipped ??
        canvas?.Scale(1, -1);
        canvas?.Translate(0, -winsize.y);

        Starry.log("Skia has loaded");
    }

    internal static void cleanup()
    {
        skpaint?.Dispose();
        surface?.Dispose();
        grContext?.Dispose();
        Starry.log("Skia has been annihilated");
    }

    internal static void calcScale(vec2i size)
    {
        scale = (int)Math.Min(size.x / Starry.settings.renderSize.x, size.y /
            Starry.settings.renderSize.y);
        offset = ((size - (Starry.settings.renderSize * (vec2)(scale, scale))) *
            (0.5, 0.5)).round();
    }

    internal static void resizeTarget(vec2i size)
    {
        // delete the old stuff
        surface?.Dispose();
        renderTarget?.Dispose();

        // and make new shit
        GRGlFramebufferInfo frameBufferInfo = new(0, SKColorType.Rgba8888.ToGlSizedFormat());
        renderTarget = new((int)size.x, (int)size.y, 0, 8, frameBufferInfo);
        
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.TopLeft,
            SKColorType.Rgba8888);
        canvas = surface.Canvas;

        // why is it flipped ??
        canvas?.Scale(1, -1);
        canvas?.Translate(0, -size.y);
    }

    /// <summary>
    /// ends drawing. this must be called for skia stuff to actually happen
    /// </summary>
    public static unsafe void endDrawing()
    {
        canvas?.Flush();
        grContext?.Flush();
        Window.glfw?.SwapBuffers(Window.window);
    }
}