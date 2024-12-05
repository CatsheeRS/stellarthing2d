using System;
using Silk.NET.OpenGL;
using SkiaSharp;
namespace starry;

public static partial class Graphics {
    internal static SKCanvas? canvas;
    internal static SKSurface? surface;
    internal static GRContext? grContext;

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

        Starry.log("Skia has loaded");
    }

    internal static void cleanup()
    {
        surface?.Dispose();
        grContext?.Dispose();
        Starry.log("Skia has been annihilated");
    }

    public static void clear(color color)
    {
        canvas?.Clear(new SKColor(color.r, color.g, color.b, color.a));
    }

    public static unsafe void endDrawing()
    {
        canvas?.Flush();
        grContext?.Flush();
        Window.glfw?.SwapBuffers(Window.window);
    }

    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        //canvas?.DrawTexture()
    }
}