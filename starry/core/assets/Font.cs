using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// it's a sprite font
/// </summary
public class Font : IAsset {
    internal Texture2D texture;

    public void load(string path) => texture = Raylib.LoadTexture(path);
    public void cleanup() => Raylib.UnloadTexture(texture);
}