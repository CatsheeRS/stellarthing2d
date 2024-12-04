using System.IO;
using StbImageSharp;

namespace starry;

public interface IAsset {
    public void load(string path);
    public void cleanup();
}

public record class Sprite: IAsset {
    internal ImageResult stbimg = new();

    public unsafe void load(string path)
    {
        using var stream = File.OpenRead(path);
        stbimg = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
    }

    public void cleanup() {}
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