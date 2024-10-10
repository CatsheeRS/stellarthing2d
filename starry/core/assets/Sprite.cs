using System.Numerics;
using SDL2;
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
    internal nint texturePtr;

    public void load(string path) => (texturePtr, size) = Platform.loadTexture(path);
    public void cleanup() => Platform.cleanupTexture(texturePtr);
}