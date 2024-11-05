using System;
// help
using static starry.Starry;
using static SDL2.SDL;
using SDL2;
namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it
/// </summary>
public static partial class Platform {
    public static int renderScale { get; set; }
    public static vec2i offset { get; set; }

    internal static void createRendererSubsystemThing()
    {
        vec2 ü = getScreenSize();
        renderScale = (int)Math.Min(ü.x / platsettings.renderSize.x, ü.y / platsettings.renderSize.y);
        offset = ((ü - settings.renderSize * vec2(renderScale, renderScale)) * vec2(0.5f, 0.5f)).round();
    }

    /// <summary>
    /// gets the screen size in pixels. this will return the scaled sized if <c>WindowSettings.highDpi</c> is disabled
    /// </summary>
    public static vec2i getScreenSize()
    {
        SDL_GL_GetDrawableSize(window, out int w, out int h);
        return vec2i(w, h);
    }

    public static void drawTexture(Sprite texture, vec2i pos, vec2i size, color tint)
    {
        SDL_Rect src = new() { x = 0, y = 0, w = texture.size.x, h = texture.size.y };
        SDL_Rect dst = new() { x = (pos.x * renderScale) + offset.x, y = (pos.y * renderScale) +
            offset.y, w = size.x * renderScale, h = size.y * renderScale };
        
        SDL_SetTextureColorMod(texture.ytfytyt, tint.r, tint.g, tint.b);
        SDL_SetTextureAlphaMod(texture.ytfytyt, tint.a);
        SDL_SetTextureBlendMode(texture.ytfytyt, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL_RenderCopy(sdlRender, texture.ytfytyt, ref src, ref dst);
        SDL_SetTextureColorMod(texture.ytfytyt, 255, 255, 255);
        SDL_SetTextureAlphaMod(texture.ytfytyt, 255);
    }
}
