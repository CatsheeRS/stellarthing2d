using System;
using SkiaSharp;
namespace starry;

/// <summary>
/// it's an asset. can be loaded or deleted. please note assets are stored in a dictionary so they're only loaded once and only deleted at the end of the game.
/// </summary>
public interface IAsset {
    /// <summary>
    /// you're supposed to call this through Starry.load<T>()
    /// </summary>
    public void load(string path);
    /// <summary>
    /// calling this doesn't modify the asset dictionary so don't use this unless you know what you're doing
    /// </summary>
    public void cleanup();
}

/// <summary>
/// it's an image. supported formats are png, jpeg, gif, bpm, webm, wbmb, ico, pkm, ktx, astc, dng, heif, and avif. please note this is implemented through skia so if you want more formats complain to them instead. generally a good idea to only use png though
/// </summary>
public record class Sprite : IAsset {
    public void load(string path)
    {
        SKBitmap bitmap = SKBitmap.Decode(path);
        if (bitmap == null) {

        }
    }

    public void cleanup()
    {
        throw new NotImplementedException();
    }
}

/*public record class Font: IAsset {
    internal Raylib_cs.Font rl;

    public unsafe void load(string path)
    {
        Raylib.font
        rl = Raylib.LoadFont(path);
    }

    public void cleanup() => Raylib.UnloadFont(rl);
}*/