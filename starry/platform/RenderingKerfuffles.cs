using System;
// help
using static starry.Starry;
using static SDL2.SDL;
using static SDL2.SDL_image;
using SDL2;
namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it
/// </summary>
public static partial class Platform
{
    /// <summary>
    /// returns a texture pointer and a size
    /// </summary>
    public unsafe static (nint, vec2i) loadTexture(string rawpath)
    {
        nint surf = IMG_Load(rawpath);
        nint optsurf = SDL_ConvertSurface(surf, ((SDL_Surface*)screenSurface.ToPointer())->format, 0);
        SDL_FreeSurface(surf);
        nint m = SDL_CreateTextureFromSurface(sdlRender, optsurf);
        SDL_FreeSurface(optsurf);
        SDL_QueryTexture(m, out uint idontwant1, out int idontwant2, out int w, out int h);
        return (m, vec2i(w, h));
    }

    /// <summary>
    /// deletes the texture :D
    /// </summary>
    public static void cleanupTexture(nint id)
    {
        SDL_DestroyTexture(id);
    }

    /// <summary>
    /// gets the screen size in pixels. this will return the scaled sized if <c>WindowSettings.highDpi</c> is disabled
    /// </summary>
    public static vec2i getScreenSize()
    {
        SDL_GetWindowSize(window, out int w, out int h);
        return vec2i(w, h);
    }

    public static void renderTexture(Sprite texture, vec2i srcPos, vec2i srcSize, vec2i destPos, vec2i destSize,
    double rotation, vec2i origin, color tint)
    {
        var fuckingsrc = new SDL_Rect() { x = srcPos.x, y = srcPos.y, w = srcSize.x, h = srcSize.y };
        var fuckingdst = new SDL_Rect() { x = destPos.x, y = destPos.y, w = destSize.x, h = destSize.y };
        var fuckingorigin = new SDL_Point() { x = origin.x, y = origin.y };

        // sdl doesn't have a tint parameter in the shits 🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑
        SDL_SetTextureColorMod(texture.texturePtr, tint.r, tint.g, tint.b);
        SDL_SetTextureAlphaMod(texture.texturePtr, tint.a);

        SDL_RenderCopyEx(sdlRender, texture.texturePtr, ref fuckingsrc, ref fuckingdst, rotation, ref fuckingorigin, 0);

        // X calls Y but does not use the HRESULT or error code that the method returns. This could lead to unexpected behavior in error conditions or low-resource situations. Use the result in a conditional statement, assign the result to a variable, or pass it as an argument to another method. (CA1806)
    }

    /// <summary>
    /// draws a rectangle :D
    /// </summary>
    public static void renderRectangle(vec2i pos, vec2i size, color color)
    {
        SDL_SetRenderDrawColor(sdlRender, color.r, color.g, color.b, color.a);
        SDL_SetRenderDrawBlendMode(sdlRender, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        var fk = new SDL_Rect() { x = pos.x, y = pos.y, h = size.x, w = size.y };
        SDL_RenderFillRect(sdlRender, ref fk);
        SDL_SetRenderDrawColor(sdlRender, 0, 0, 0, 255);
        SDL_SetRenderDrawBlendMode(sdlRender, SDL_BlendMode.SDL_BLENDMODE_NONE);
    }
}
