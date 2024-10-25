using System;
using System.IO;
using StbImageSharp;
using static starry.Starry;
using static SDL2.SDL;
namespace starry;

/// <summary>
/// it's a sprite
/// </summary>
public class Sprite : IAsset {
    /// <summary>
    /// the size of the texture, in pixels
    /// </summary>
    public vec2i size { get; internal set; }
    /// <summary>
    /// pixel data for the pixels. you can edit this property to edit sprites
    /// </summary>
    public color[,] data { get; set; } = new color[0, 0];
    public nint ytfytyt { get; set; }

    public unsafe void load(string path) {
        using var stream = File.OpenRead(path);
        ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        size = vec2i(image.Width, image.Height);
        
        nint crapper;
        fixed (byte* crap = image.Data) {
            crapper = SDL_CreateRGBSurfaceFrom((nint)crap, image.Width, image.Height, 4 * 8, image.Width * 4,
                0x000000FF, 0x0000FF00, 0x00FF0000, 0xFF000000);
        }

        ytfytyt = SDL_CreateTextureFromSurface(Platform.sdlRender, crapper);
        SDL_FreeSurface(crapper);
    }

    // this sprite shit isn't based on sdl, the garbage collector will catch it
    public void cleanup() {}
}