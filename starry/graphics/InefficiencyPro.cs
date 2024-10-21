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
                videobuf[x, y] = color.black;
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
        // TODO: don't (scale crap)
        var gay = new SDL_Rect() { x = 0, y = 0, w = platsettings.renderSize.x * 3, h = platsettings.renderSize.y * 3 };
        SDL_RenderCopy(sdlRender, rendertarget, ref fuckimsigma, ref gay);
    }

    /// <summary>
    /// gets the screen size in pixels. this will return the scaled sized if <c>WindowSettings.highDpi</c> is disabled
    /// </summary>
    public static vec2i getScreenSize()
    {
        SDL_GetWindowSize(window, out int w, out int h);
        return vec2i(w, h);
    }

    public static void clearScreen(color color)
    {
        for (int y = 0; y < platsettings.renderSize.y; y++) {
            for (int x = 0; x < platsettings.renderSize.x; x++) {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                videobuf[x, y] = color;
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }

    public static void renderTexture(Sprite texture, vec2i pos)
    {
        for (int y = 0; y < texture.size.y; y++) {
            for (int x = 0; x < texture.size.x; x++) {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                videobuf[x + pos.x, y + pos.y] = texture.data[x, y];
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}
