using System;
using System.Collections.Generic;
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
    public unsafe static void setupTriangle(float[] verts, ushort[] indices)
    {
        uint vertexBufferId = 0;
        gl?.GenBuffers(1, &vertexBufferId);
        gl?.BindBuffer(BufferTargetARB.ArrayBuffer, vertexBufferId);
        fixed (float* p = verts) {
            void* nada = p;
            gl?.BufferData(BufferTargetARB.ArrayBuffer, (nuint)System.Buffer.ByteLength(verts), nada, BufferUsageARB.StaticDraw);
        }
        gl?.EnableVertexAttribArray(0);
        gl?.VertexAttribPointer(0, 2, GLEnum.Float, false, sizeof(float) * 5, 0);
        gl?.EnableVertexAttribArray(1);
        gl?.VertexAttribPointer(1, 3, GLEnum.Float, false, sizeof(float) * 5, sizeof(float) * 2);

        uint indexBufferId = 0;
        gl?.GenBuffers(1, &indexBufferId);
        gl?.BindBuffer(BufferTargetARB.ElementArrayBuffer, indexBufferId);
        fixed (ushort* p = indices) {
            void* nguh = p;
            gl?.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)System.Buffer.ByteLength(indices), nguh, BufferUsageARB.StaticDraw);
        }
    }

    /// <summary>
    /// draws a triangle lmao
    /// </summary>
    public static void drawTriangle()
    {
        gl?.DrawArrays(GLEnum.Triangles, 0, 6);
        var whytho = 0;
        gl?.DrawElements(GLEnum.Triangles, 6, DrawElementsType.UnsignedShort, ref whytho);
    }

    /// <summary>
    /// compiles and links the provided shaders into a program
    /// </summary>
    public static void installShader(params IShader[] shaders)
    {
        Stack<uint> shaderIds = [];
        foreach (IShader shader in shaders) {
            GLEnum type = shader.getType() switch {
                ShaderType.vertex => GLEnum.VertexShader,
                ShaderType.fragment => GLEnum.FragmentShader,
                _ => throw new Exception(),
            };

            uint shaderid = gl?.CreateShader(type) ?? 0;
            gl?.ShaderSource(shaderid, shader.getSource());
            gl?.CompileShader(shaderid);
            shaderIds.Push(shaderid);
        }

        uint programid = gl?.CreateProgram() ?? 0;
        while (shaderIds.Count > 0) gl?.AttachShader(programid, shaderIds.Pop());
        gl?.LinkProgram(programid);
        gl?.UseProgram(programid);
    }
}