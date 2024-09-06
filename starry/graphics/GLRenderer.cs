using System;
using Silk.NET.GLFW;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using static starry.Starry;

namespace starry;

/// <summary>
/// since starry only supports opengl, this handles all things rendering. the coordinate system works like this: (0, 0) is the center, (-1, -1) is bottom left, and (1, 1) is top right.
/// </summary>
public static class GLRenderer {
    static GL? gl;
    static Glfw? glfw;
    unsafe static WindowHandle* window;

    /// <summary>
    /// sets up the renderer
    /// </summary>
    public unsafe static void create(GL gl, Glfw glfw, WindowHandle* window)
    {
        GLRenderer.gl = gl;
        GLRenderer.glfw = glfw;
        GLRenderer.window = window;
        glfw.GetWindowSize(window, out int winwidth, out int winheight);
        gl.Viewport(0, 0, (uint)winwidth, (uint)winheight);
    }

    /// <summary>
    /// setups a triangle. for efficiency, every vertice must be in pairs of X (first) and Y (second).
    /// </summary>
    public unsafe static void setupTriangle(float[] verts)
    {
        uint bufferid = 0;
        gl?.GenBuffers(1, &bufferid);
        gl?.BindBuffer((GLEnum)BufferTargetARB.ArrayBuffer, bufferid);
        fixed (float* p = verts) {
            void* nada = p;
            gl?.BufferData(BufferTargetARB.ArrayBuffer, (nuint)System.Buffer.ByteLength(verts), nada, BufferUsageARB.StaticDraw);
        }
        gl?.EnableVertexAttribArray(0);
        gl?.VertexAttribPointer(0, 2, GLEnum.Float, false, 0, 0);
    }

    /// <summary>
    /// draws a triangle lmao
    /// </summary>
    public static void drawTriangle()
    {
        gl?.DrawArrays(GLEnum.Triangles, 0, 3);
    }
}