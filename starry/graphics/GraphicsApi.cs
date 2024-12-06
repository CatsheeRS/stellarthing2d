using System;
using SkiaSharp;
namespace starry;

/// <summary>
/// the renderer. this is just skia lamo
/// </summary>
public static partial class Graphics {
    /// <summary>
    /// clears screen. useful for when you don't want your game to look like you're on drugs
    /// </summary>
    public static void clear(color color)
    {
        canvas?.Clear(new SKColor(color.r, color.g, color.b, color.a));
    }

    /// <summary>
    /// ends drawing. useful for when your game has graphics
    /// </summary>
    public static unsafe void endDrawing()
    {
        skpaint?.Dispose();
        canvas?.Flush();
        grContext?.Flush();
        Window.glfw?.SwapBuffers(Window.window);
    }

    /// <summary>
    /// draws a sprite. rects are in game render coordinates (no camera) and rotation is in degrees. src and dst are so you can draw a portion of the sprite. origin is from 0 to 1, with (0, 0) being the top left and (0.5, 0.5) being the center
    /// </summary>
    public static void drawSprite(Sprite sprite, vec2 pos, vec2 origin,
    double rotation, color tint)
    {
        if (skpaint == null) return;
        if (!sprite.isValid()) {
            Starry.log($"Sprite at {sprite.path} is invalid; cannot draw");
            return;
        }
        
        canvas?.Save();
        
        // rotation
        canvas?.Translate((float)(sprite.size.x * origin.x), (float)(sprite.size.y * origin.y));
        canvas?.RotateDegrees((float)rotation);
        canvas?.Translate(-(float)(sprite.size.x * origin.x), -(float)(sprite.size.y * origin.y));

        // tint
        var colorfilter = SKColorFilter.CreateBlendMode(
            new SKColor(tint.r, tint.g, tint.b, tint.a), SKBlendMode.Modulate);
        
        using var newpaint = new SKPaint() {
            IsAntialias = false,
            ColorFilter = colorfilter,
        };

        // draw.
        canvas?.DrawImage(sprite.skimg, SKRect.Create((float)pos.x, (float)pos.y, sprite.size.x, 
            sprite.size.y), newpaint);
        
        // reset the canvas for other shits
        canvas?.Restore();
    }
}