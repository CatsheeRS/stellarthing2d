using frambos.core;
using frambos.util;
using Silk.NET.Maths;
using Silk.NET.SDL;

namespace frambos.graphics;

public static class Renderer {
    public static double scale_factor { get; internal set; } = 1;
    internal static double center_offset;

    /// <summary>
    /// draws a texture and stretches it to fit the size; rotation is in degrees; center is used for rotation (divide the size by 2 so it's in the center); alpha must be between 0 and 1 with 0 being completely transparent
    /// </summary>
    public static unsafe void draw_texture(core.Texture texture, Vector2 pos, Vector2 size, double rotation,
    Vector2 center, bool flip_x = false, bool flip_y = false)
    {
        // quite the mouthful
        Rectangle<int> rect = new((int)pos.x + (int)center_offset, (int)pos.y, (int)(size.x * scale_factor), (int)(size.y * scale_factor));
        Point sdl_center = new((int)center.x, (int)center.y);
        
        // figure out flipping
        RendererFlip flip = RendererFlip.None;
        if (flip_x && !flip_y) flip = RendererFlip.Horizontal;
        else if (!flip_x && flip_y) flip = RendererFlip.Vertical;
        else if (flip_x && flip_y) flip = RendererFlip.Horizontal | RendererFlip.Vertical;

        MainLoop.sdl.RenderCopyEx(
            MainLoop.render, // renderer
            texture.sdl_texture, // texture
            null, // source rect (null for whole texture)
            ref rect, // destination rect,
            rotation, // angle
            ref sdl_center, // center
            flip // flip :)
        );
    }
}