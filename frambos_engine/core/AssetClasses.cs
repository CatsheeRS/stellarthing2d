using Silk.NET.SDL;

namespace frambos.core;

public interface IAsset {
    IAsset load(string path);
}

public class Texture : IAsset {
    internal Texture sdl_texture { get; set; }

    public IAsset load(string path)
    {
        
    }
}