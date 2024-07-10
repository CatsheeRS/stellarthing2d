using frambos.core;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace frambos.graphics;

public static class Renderer {
    public static GL gl { get; set; }

    /// <summary>
    /// starts opengl
    /// </summary>
    public static void setup(GL gl)
    {
        Frambos.log("setting up opengl");
        Renderer.gl = gl;
        gl.Viewport(0, 0, 1280, 720);
    }
}