using System.IO;
using frambos.util;
using Silk.NET.SDL;
using StbImageSharp;

namespace frambos.core;

/// <summary>
/// interface all assets must follow to work with AssetManager, but don't use it with saves though :)
/// </summary>
public interface IAsset {
    /// <summary>
    /// loads the asset, keep in mind that AssetManager only loads it once then keeps it in a dictionary
    /// </summary>
    IAsset load(string path);
    /// <summary>
    /// frees the asset, around the time the game is closing
    /// </summary>
    void dispose();
}

/// <summary>
/// safe wrapper around sdl textures
/// </summary>
public class Texture : IAsset {
    internal unsafe Silk.NET.SDL.Texture* sdl_texture { get; set; }
    public Vector2 size { get; private set; }

    public unsafe IAsset load(string path)
    {
        ImageResult res = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);
        
        Surface* surf;
        fixed (byte* byte_ptr = res.Data) {
            void* data = byte_ptr;
            surf = MainLoop.sdl.CreateRGBSurfaceFrom(data, res.Width, res.Height, 24, res.Width * 3,
                0x000000FF, 0x0000FF00, 0x00FF0000, 0x00000000);
        }

        sdl_texture = MainLoop.sdl.CreateTextureFromSurface(MainLoop.render, surf);
        MainLoop.sdl.FreeSurface(surf);
        return new Texture { sdl_texture = sdl_texture, size = new Vector2(res.Width, res.Height) };
    }

    public unsafe void dispose()
    {
        MainLoop.sdl.DestroyTexture(sdl_texture);
    }
}