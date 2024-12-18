using System;
using System.IO;
using SkiaSharp;
namespace starry;

/// <summary>
/// interface for sprites (sprite, tile sprite, animation sprite)
/// </summary>
public interface ISprite {
    /// <summary>
    /// returns the internal representation of an image
    /// </summary>
    public SKImage? getInternalImage();
    /// <summary>
    /// if true, the internal representation is valid
    /// </summary>
    public bool isInternalValid();
    /// <summary>
    /// in pixels
    /// </summary>
    public vec2i getSize();
    /// <summary>
    /// frees the internal image
    /// </summary>
    public void cleanupInternal();
}

/// <summary>
/// it's an image. supported formats are png, jpeg, gif, bpm, webm, wbmb, ico, pkm, ktx, astc, dng, heif, and avif. please note this is implemented through skia so if you want more formats complain to them instead. generally a good idea to only use png though
/// </summary>
public record class Sprite: IAsset, ISprite {
    internal SKBitmap? skbmp;
    internal SKImage? skimg;
    /// <summary>
    /// it's the path.
    /// </summary>
    public string path { get; private set; } = "";

    public void load(string path)
    {
        Graphics.actions.Enqueue(() => {
            skbmp = SKBitmap.Decode(path);
            if (skbmp == null) {
                Starry.log($"Couldn't load {path} as sprite");
                return;
            }

            skimg = SKImage.FromBitmap(skbmp);

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

    public void cleanupInternal() => cleanup();
    public bool isInternalValid() => skbmp != null && skimg != null;
    public SKImage? getInternalImage() => skimg;
    public vec2i getSize() => (skbmp?.Width ?? 0, skbmp?.Height ?? 0);
}

/// <summary>
/// it's a sprite but with multiple sides. the sprite must be a png (can't be bothered) with filenames ending with the side's letter ((l)eft, (r)ight, (t)op, (b)ottom)
/// </summary>
public record class TileSprite: ISprite {
    public ISprite left { get; internal set; }
    public ISprite right { get; internal set; }
    public ISprite top { get; internal set; }
    public ISprite bottom { get; internal set; }
    /// <summary>
    /// side.
    /// </summary>
    public TileSide side { get; set; }

    public TileSprite(ISprite left, ISprite right, ISprite top, ISprite bottom)
    {
        this.left = left;
        this.right = right;
        this.top = top;
        this.bottom = bottom;
    }

    public void cleanup()
    {
        left.cleanupInternal();
        right.cleanupInternal();
        top.cleanupInternal();
        bottom.cleanupInternal();
    }

    public void cleanupInternal() => cleanup();
    public bool isInternalValid() => left.isInternalValid() && right.isInternalValid() &&
                                     top.isInternalValid() && bottom.isInternalValid();
    public vec2i getSize() => left.getSize();
    public SKImage? getInternalImage()
    {
        return (side switch {
            TileSide.left => left,
            TileSide.right => right,
            TileSide.top => top,
            TileSide.bottom => bottom,
            _ => throw new Exception("moron"),
        }).getInternalImage();
    }
}