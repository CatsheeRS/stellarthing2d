using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// it's a font (ttf, not a sprite font)
/// </summary
public class Font : IAsset {
    internal Raylib_cs.Font rlfont;

    public void load(string path) => rlfont = Raylib.LoadFontEx(path, 16, [], 250);
    public void cleanup() => Raylib.UnloadFont(rlfont);
}