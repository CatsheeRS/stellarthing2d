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
    internal static color[,]? atlasdata;

    internal unsafe static void loadAtlas(string path)
    {
        //var surf = (SDL_Surface*)IMG_Load(path).ToPointer();
        //surf->pixels.ToPointer();
    }
}