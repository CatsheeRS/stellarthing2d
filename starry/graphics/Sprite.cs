using System;
using System.IO;
using Silk.NET.OpenGL;
using StbImageSharp;
namespace starry;

public record class Sprite: IAsset {
    public vec2i size { get; private set; }
    internal ImageResult stbimg = new();
    internal uint id;
    internal GLEnum internalFormat = GLEnum.Rgb;
    internal GLEnum imageFormat = GLEnum.Rgb;
    internal GLEnum wraps = GLEnum.Repeat;
    internal GLEnum wrapt = GLEnum.Repeat;
    internal GLEnum filterMin = GLEnum.Linear;
    internal GLEnum filterMax = GLEnum.Linear;

    public void load(string path)
    {
        using var stream = File.OpenRead(path);
        stbimg = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        size = (stbimg.Width, stbimg.Height);
    }

    public void cleanup() {}

    public unsafe void generate()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        // create texture
        gl.BindTexture(GLEnum.Texture2D, id);
        // i love that every opengl call is comically large
        
        fixed (void* ptr = stbimg.Data) {
            gl.TexImage2D(GLEnum.Texture2D, 0, (int)internalFormat, (uint)size.x, (uint)size.y, 0,
                imageFormat, GLEnum.UnsignedByte, ptr);
        }

        // set Texture wrap and filter modes
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)wraps);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)wrapt);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)filterMin);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)filterMax);
        // unbind texture
        gl.BindTexture(GLEnum.Texture2D, 0);
    }

    public void bind()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;
        gl.BindTexture(GLEnum.Texture2D, id);
    }
}