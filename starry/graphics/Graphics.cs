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
        
        surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft,
            SKColorType.Rgba8888);

        canvas = surface.Canvas;

        // sick pain(t) stuff
        skpaint = new SKPaint() {
            Color = SKColors.White,
            IsAntialias = false, // this is a pixel art game
        };

        Starry.log("Skia has loaded");
    }

    internal static void cleanup()
    {
        surface?.Dispose();
        grContext?.Dispose();
        Starry.log("Skia has been annihilated");
    }

    /// <summary>
    /// clears screen. useful for when you don't want your game to look like you're on drugs
    /// </summary>
    /// <param name="color"></param>
    public static void clear(color color)
    {
        canvas?.Clear(new SKColor(color.r, color.g, color.b, color.a));
    }

    /// <summary>
    /// ends drawing. useful for when your game has graphics
    /// </summary>
    public static unsafe void endDrawing()
    {
        canvas?.Flush();
        grContext?.Flush();
        Window.glfw?.SwapBuffers(Window.window);
    }

    /// <summary>
    /// draws a sprite. rect is in game render coordinates (no camera) and rotation is in degrees.
    /// </summary>
    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        //canvas?.DrawTexture()
    }
}