using System.Numerics;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// it's a sprite
/// </summary>
public class Sprite : IAsset {
    /// <summary>
    /// the size of the texture, in pixels
    /// </summary>
    public vec2i size { get; internal set; }
    internal Texture2D rlSprite;

    public void load(string path)
    {
        rlSprite = Raylib.LoadTexture(path);
        size = vec2i(rlSprite.Width, rlSprite.Height);
    }
    public void cleanup() => Raylib.UnloadTexture(rlSprite);
}