using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
namespace starry;

public static partial class Graphics {
    internal static GL? gl;
    internal static Shader shader = new();
    internal static uint quadVao;
    internal static Queue<IGlCall> drawCalls = [];

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

        // shade
        shader.compile(
            File.ReadAllText(Path.GetFullPath("assets/shaders/vertex.glsl")),
            File.ReadAllText(Path.GetFullPath("assets/shaders/fragment.glsl"))
        );
        shader.use();
        shader.setInt("image", 0);
        Matrix4x4 proj = Starry.ortho(0, Starry.settings.renderSize.x, Starry.settings.renderSize.y,
            0, -1, 1);
        shader.setMat4("projection", MemoryMarshal.Cast<Matrix4x4, float>(
            MemoryMarshal.CreateSpan(ref proj, 1)));

        Starry.log("Sprite renderer has been initialized");
    }

    public static void clear(color color)
    {
        if (gl == null) return;
        gl.ClearColor(color.r / 256f, color.g / 256f, color.b / 256f, color.a / 256f);
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static unsafe void endDrawing()
    {
        if (gl == null) return;

        // we need to run shit at the end of the frame since opengl isn't multithreaded
        foreach (IGlCall draw in drawCalls) {
            draw.run();
        }

        Window.glfw?.SwapBuffers(Window.window);
    }

    public static void cleanup() {}

    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        // quite the mouthful
        drawCalls.Enqueue(new SpriteDrawCall(sprite, Starry.transform2matrix(rect, rotation), color));
    }

    internal static void batchRender(SpriteDrawCall draw)
    {
        if (gl == null) return;
        shader.use();

        // man
        Matrix4x4 thisisbeginningtohurt = draw.transform;
        // M but S
        ReadOnlySpan<float> silkdotnetdoesntletmepassamatrix =
            MemoryMarshal.Cast<Matrix4x4, float>(MemoryMarshal.CreateSpan(
            ref thisisbeginningtohurt, 1));
        
        shader.setMat4("model", silkdotnetdoesntletmepassamatrix);
        shader.setVec3("spriteColor", (draw.color.r, draw.color.g, draw.color.b));

        // homicide
        gl.ActiveTexture(GLEnum.Texture0);
        draw.sprite.bind();

        gl.BindVertexArray(quadVao);
        gl.DrawArrays(GLEnum.Triangles, 0, 6);
        gl.BindVertexArray(0);
    }
}