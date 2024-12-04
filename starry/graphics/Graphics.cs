using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
namespace starry;

public static partial class Graphics {
    internal static GL? gl;
    internal static Shader shader;
    internal static uint quadVao;

    public static unsafe void create()
    {
        // shut up
        if (Window.glfw == null) return;

        gl = GL.GetApi(Window.glfw.GetProcAddress);
        Window.glfw.GetWindowSize(Window.window, out int wx, out int wy);
        gl.Viewport(0, 0, (uint)wx, (uint)wy);

        Starry.log("OpenGL has been initialized");

        setupSpriteStuff();
    }

    static unsafe void setupSpriteStuff()
    {
        if (gl == null) return;
        // i stole this from https://learnopengl.com/In-Practice/2D-Game/Rendering-Sprites :D
        float[] vertices = [
            // pos      // tex
            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,

            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f
        ];

        gl.GenVertexArrays(1, out quadVao);
        gl.GenBuffers(1, out uint vbo);
        
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
        gl.BufferData<float>(BufferTargetARB.ArrayBuffer, vertices.AsSpan(),
            BufferUsageARB.StaticDraw);

        gl.BindVertexArray(quadVao);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 4, GLEnum.Float, false, 4 * sizeof(float), null);
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);  
        gl.BindVertexArray(0);

        Starry.log("Sprite renderer has been initialized");
    }

    public static void clear(color color)
    {
        if (gl == null) return;
        gl.ClearColor(color.r / 256, color.g / 256, color.b / 256, color.a / 256);
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static unsafe void endDrawing()
    {
        Window.glfw?.SwapBuffers(Window.window);
    }

    public static void cleanup() {}

    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        if (gl == null) return;
        shader.use();

        // i'm in pain!
        Matrix4x4 model = Matrix4x4.Identity;
        model *= Matrix4x4.CreateTranslation((float)rect.x, (float)rect.y, 0);

        model *= Matrix4x4.CreateTranslation(0.5f * (float)rect.w, 0.5f * (float)rect.h, 0);
        model *= Matrix4x4.CreateRotationZ((float)rotation); // TODO degree to radian
        model *= Matrix4x4.CreateScale(-0.5f * (float)rect.w, -0.5f * (float)rect.h, 0);

        model *= Matrix4x4.CreateScale((float)rect.w, (float)rect.h, 1);

        // man
        // M but S
        ReadOnlySpan<float> mbuts =
            MemoryMarshal.Cast<Matrix4x4, float>(MemoryMarshal.CreateSpan(ref model, 1));
        
        shader.setMat4("model", mbuts);
        shader.setVec3("spriteColor", (color.r, color.g, color.b));

        // homicide
        gl.ActiveTexture(GLEnum.Texture0);
        sprite.bind();

        gl.BindVertexArray(quadVao);
        gl.DrawArrays(GLEnum.Triangles, 0, 6);
        gl.BindVertexArray(0);
    }
}