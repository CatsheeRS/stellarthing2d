using System;
using System.IO;
using Silk.NET.OpenGL;
using StbImageSharp;
namespace starry;

public record class Sprite: IAsset {
    public vec2i size { get; private set; }
    internal ImageResult stbimg = new();
    internal uint id = 0;
    internal GLEnum internalFormat = GLEnum.Rgb;
    internal GLEnum imageFormat = GLEnum.Rgb;
    internal GLEnum wraps = GLEnum.Repeat;
    internal GLEnum wrapt = GLEnum.Repeat;
    internal GLEnum filterMin = GLEnum.Linear;
    internal GLEnum filterMax = GLEnum.Linear;

    public void load(string path)
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        using var stream = File.OpenRead(path);
        stbimg = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        size = (stbimg.Width, stbimg.Height);

        MakeTextureCall lig = new(1);
        lig.onSpriteGen += (id) => this.id = id;
        Graphics.drawCalls.Enqueue(lig);
    }

    public void cleanup() {}

    public unsafe void generate() => Graphics.drawCalls.Enqueue(new GenSpriteCall(id, stbimg.Data,
        size, internalFormat, imageFormat, wraps, wrapt, filterMin, filterMax));

    public void bind() => Graphics.drawCalls.Enqueue(new BindTextureCall(id));
}