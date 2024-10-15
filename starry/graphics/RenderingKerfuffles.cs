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
    internal static color[,]? videobuf { get; set; }
    internal static nint rendertarget { get; set; }
    public static float renderScale { get; set; }

    internal static void createRendererSubsystemThing()
    {
        vec2 ü = getScreenSize();
        renderScale = (float)Math.Min(ü.x / platsettings.renderSize.x, ü.y / platsettings.renderSize.y);

        // video buffer stuff
        videobuf = new color[platsettings.renderSize.x, platsettings.renderSize.y];
        for (int y = 0; y < platsettings.renderSize.y; y++) {
            for (int x = 0; x < platsettings.renderSize.x; x++) {
                videobuf[x, y] = color.darkBlue;
            }
        }

        // more video buffer stuff
        rendertarget = SDL_CreateTexture(sdlRender, SDL_PIXELFORMAT_ABGR8888,
            (int)SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
            platsettings.renderSize.x, platsettings.renderSize.y);
    }

    internal unsafe static void processVideoBufferStuff()
    {
        // convert fucking pixel data
        byte[] socialconstruct = new byte[platsettings.renderSize.x * platsettings.renderSize.y * 4];
        int eye = 0;
        for (int y = 0; y < platsettings.renderSize.y; y++) {
            for (int x = 0; x < platsettings.renderSize.x; x++) {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                color colores = videobuf[x, y];
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                socialconstruct[eye++] = colores.r;
                socialconstruct[eye++] = colores.g;
                socialconstruct[eye++] = colores.b;
                socialconstruct[eye++] = colores.a;
            }
        }

        fixed (byte* fuck = socialconstruct) {
            SDL_UpdateTexture(rendertarget, 0, (nint)fuck, platsettings.renderSize.x);
        }

        var fuckimsigma = new SDL_Rect() { x = 0, y = 0, w = platsettings.renderSize.x, h = platsettings.renderSize.y };
        // todo: don't
        var gay = new SDL_Rect() { x = 0, y = 0, w = platsettings.renderSize.x * 3, h = platsettings.renderSize.y * 3 };
        SDL_RenderCopy(sdlRender, rendertarget, ref fuckimsigma, ref gay);
    }

    /// <summary>
    /// returns a texture pointer and a size
    /// </summary>
    public unsafe static (nint, vec2i) loadTexture(string rawpath)
    {
        /*nint surf = IMG_Load(rawpath);
        nint optsurf = SDL_ConvertSurface(surf, ((SDL_Surface*)screenSurface.ToPointer())->format, 0);
        SDL_FreeSurface(surf);
        nint m = SDL_CreateTextureFromSurface(sdlRender, optsurf);
        SDL_FreeSurface(optsurf);
        SDL_QueryTexture(m, out uint idontwant1, out int idontwant2, out int w, out int h);
        return (m, vec2i(w, h));*/
        return default;
    }

    /// <summary>
    /// deletes the texture :D
    /// </summary>
    public static void cleanupTexture(nint id)
    {
        //SDL_DestroyTexture(id);
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
        /*var fuckingsrc = new SDL_Rect() {
            x = (int)(srcPos.x * renderScale),
            y = (int)(srcPos.y * renderScale),
            w = (int)(srcSize.x * renderScale),
            h = (int)(srcSize.y * renderScale)
        };
        var fuckingdst = new SDL_Rect() {
            x = (int)(destPos.x * renderScale),
            y = (int)(destPos.y * renderScale),
            w = (int)(destSize.x * renderScale),
            h = (int)(destSize.y * renderScale)
        };
        var fuckingorigin = new SDL_Point() {
            x = (int)(origin.x * renderScale),
            y = (int)(origin.y * renderScale)
        };

        // sdl doesn't have a tint parameter in the shits 🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑🤑
        SDL_SetTextureColorMod(texture.texturePtr, tint.r, tint.g, tint.b);
        SDL_SetTextureAlphaMod(texture.texturePtr, tint.a);

        SDL_RenderCopyEx(sdlRender, texture.texturePtr, ref fuckingsrc, ref fuckingdst, rotation, ref fuckingorigin, 0);

        // X calls Y but does not use the HRESULT or error code that the method returns. This could lead to unexpected behavior in error conditions or low-resource situations. Use the result in a conditional statement, assign the result to a variable, or pass it as an argument to another method. (CA1806)*/
    }

    /// <summary>
    /// draws a rectangle :D
    /// </summary>
    public static void renderRectangle(vec2i pos, vec2i size, color color)
    {
        /*SDL_SetRenderDrawColor(sdlRender, color.r, color.g, color.b, color.a);
        SDL_SetRenderDrawBlendMode(sdlRender, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        var fk = new SDL_Rect() {
            x = (int)(pos.x * renderScale),
            y = (int)(pos.y * renderScale),
            h = (int)(size.x * renderScale),
            w = (int)(size.y * renderScale)
        };
        SDL_RenderFillRect(sdlRender, ref fk);
        SDL_SetRenderDrawColor(sdlRender, 0, 0, 0, 255);
        SDL_SetRenderDrawBlendMode(sdlRender, SDL_BlendMode.SDL_BLENDMODE_NONE);*/
    }
}
