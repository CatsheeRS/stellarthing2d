using System;
using System.IO;
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
    /// <summary>
    /// size in pixels
    /// </summary>
    public vec2i size { get; private set; } = (0, 0);
    internal SKBitmap? skbmp;
    internal SKImage? skimg;
    /// <summary>
    /// it's the path.
    /// </summary>
    public string path { get; private set; }= "";

    public void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            skbmp = SKBitmap.Decode(path);
            if (skbmp == null) {
                Starry.log($"Couldn't load {path} as sprite");
                return;
            }

            skimg = SKImage.FromBitmap(skbmp);
            size = (skbmp.Width, skbmp.Height);

            Starry.log($"Loaded sprite at {path}");
            this.path = path;
        });
        Graphics.actionLoopEvent.Set();
    }

    public void cleanup()
    {
        Graphics.actions.Enqueue(() => {
            skbmp?.Dispose();
            skimg?.Dispose();
            Starry.log($"Deleted sprite at {path}");
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// if true, the sprite is, in fact, valid
    /// </summary>
    public bool isValid() => skbmp != null && skimg != null;
}

/// <summary>
/// font
/// </summary>
public record class Font: IAsset {
    internal SKTypeface? skfnt;

    public void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            skfnt = SKTypeface.FromFile(path);
        });
        Graphics.actionLoopEvent.Set();
    }

    public void cleanup() {
        Graphics.actions.Enqueue(() => {
            skfnt?.Dispose();
        });
        Graphics.actionLoopEvent.Set();
    }
}