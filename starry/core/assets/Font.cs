using static starry.Starry;
namespace starry;

/// <summary>
/// it's a sprite font
/// </summary
public class Font : IAsset {
    internal Sprite? texture;

    public void load(string path) => texture = load<Sprite>(path);
    public void cleanup() => texture?.cleanup();
}