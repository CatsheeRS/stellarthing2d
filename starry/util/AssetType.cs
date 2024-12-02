using Raylib_cs;

namespace starry;

public interface IAsset {
    public void load(string path);
    public void cleanup();
}

public record class Font: IAsset {
    internal Raylib_cs.Font rl;

    public void load(string path) => rl = Raylib.LoadFont(path);
    public void cleanup() => Raylib.UnloadFont(rl);
}