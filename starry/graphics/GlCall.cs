using System.ComponentModel;
using System.Numerics;
using Silk.NET.OpenGL;
namespace starry;

/// <summary>
/// opengl call since opengl isn't multithreaded
/// </summary>
public interface IGlCall {
    public void run();
}

/// <summary>
/// represents a draw call for the batch rendering system
/// </summary>
public class SpriteDrawCall(Sprite sprite, Matrix4x4 transform, color color): IGlCall {
    public Sprite sprite { get; set; } = sprite;
    public Matrix4x4 transform { get; set; } = transform;
    public color color { get; set; } = color;

    public void run()
    {
        Graphics.batchRender(this);
    }
}

/// <summary>
/// generates a texture and puts the opengl id in the id property
/// </summary>
public class MakeTextureCall(uint n): IGlCall {
    public uint n { get; set; } = n;
    public uint id { get; private set; } = 0;
    public SpriteHasGenerated? onSpriteGen;

    public void run()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        gl.GenTextures(n, out uint lma);
        id = lma;
        onSpriteGen?.Invoke(id);
    }

    public delegate void SpriteHasGenerated(uint id);
}

/// <summary>
/// ge
/// </summary>
public class GenSpriteCall(uint id, byte[] data, vec2i size, GLEnum inf, GLEnum imf, GLEnum ws,
GLEnum wt, GLEnum fmn, GLEnum fmx): IGlCall {
    public uint id { get; set; } = id;
    public byte[] data { get; set; } = data;
    public vec2i size { get; set; } = size;
    public GLEnum inFormat { get; set; } = inf;
    public GLEnum imFormat { get; set; } = imf;
    public GLEnum wraps { get; set; } = ws;
    public GLEnum wrapt { get; set; } = wt;
    public GLEnum fmin { get; set; } = fmn;
    public GLEnum fmax { get; set; } = fmx;

    public unsafe void run()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        // create texture
        gl.BindTexture(GLEnum.Texture2D, id);

        // i love that every opengl call is comically large
        fixed (void* ptr = data) {
            gl.TexImage2D(GLEnum.Texture2D, 0, (int)inFormat, (uint)size.x, (uint)size.y, 0,
                imFormat, GLEnum.UnsignedByte, ptr);
        }

        // set texture wrap and filter modes
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)wraps);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)wrapt);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)fmin);
        gl.TexParameter(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)fmax);

        // unbind texture
        gl.BindTexture(GLEnum.Texture2D, 0);
    }
}

public class BindTextureCall(uint id): IGlCall {
    public uint id { get; set; } = id;

    public void run()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        gl.BindTexture(GLEnum.Texture2D, id);
    }
}

public class ClearCall(color color): IGlCall {
    // color
    public color color { get; set; } = color;

    public void run()
    {
        if (Graphics.gl == null) return;
        GL gl = Graphics.gl;

        gl.ClearColor(color.r / 256f, color.g / 256f, color.b / 256f, color.a / 256f);
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }
}