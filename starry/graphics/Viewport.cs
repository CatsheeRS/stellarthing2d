using static starry.Starry;
using static SDL2.SDL;
using SDL2;
namespace starry;

/// <summary>
/// it clips stuff and renders to a texture and stuff (implemented directly tghrough sdl instead of through platform since fuck me)
/// </summary>
public class Viewport(vec2i size)
{
    internal nint textur = SDL_CreateTexture(Platform.sdlRender, SDL_PIXELFORMAT_RGBA8888,
                           (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, size.x, size.y);
    
    /// <summary>
    /// starts rendering to the fucking viewport (pair it with end() and then use render() to make it actually show up)
    /// </summary>
    public void start(color clearColor)
    {
        SDL_SetRenderTarget(Platform.sdlRender, textur);
        SDL_SetRenderDrawColor(Platform.sdlRender, clearColor.r, clearColor.g, clearColor.b, clearColor.a);
    }

    /// <summary>
    /// ends rendering to the fucking viewport (pair it with start() and then use render() to make it actually show up)
    /// </summary>
    public void end()
    {
        // go back to rendering to the window
        SDL_SetRenderTarget(Platform.sdlRender, 0);
    }

    /// <summary>
    /// makes shit actually show up and stuff (the src parameters are for rendering a part of the viewport instead of the whole thing, origin is in pixels, rotation is in angles)
    /// </summary>
    public void render(vec2i srcPos, vec2i srcSize, vec2i destPos, vec2i destSize, double rotation, vec2i origin,
    color tint)
    {
        var fuckingsrc = new SDL_Rect() { x = srcPos.x, y = srcPos.y, w = srcSize.x, h = srcSize.y };
        var fuckingdst = new SDL_Rect() { x = destPos.x, y = destPos.y, w = destSize.x, h = destSize.y };
        var fuckingorigin = new SDL_Point() { x = origin.x, y = origin.y };

        // sdl doesn't have a tint parameter in the shits 🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑
        SDL_SetTextureColorMod(textur, tint.r, tint.g, tint.b);
        SDL_SetTextureAlphaMod(textur, tint.a);

        SDL_RenderCopyEx(Platform.sdlRender, textur, ref fuckingsrc, ref fuckingdst, rotation, ref fuckingorigin, 0);

        // X calls Y but does not use the HRESULT or error code that the method returns. This could lead to unexpected behavior in error conditions or low-resource situations. Use the result in a conditional statement, assign the result to a variable, or pass it as an argument to another method. (CA1806)
    }

    ~Viewport()
    {
        SDL_DestroyTexture(textur);
    }
}