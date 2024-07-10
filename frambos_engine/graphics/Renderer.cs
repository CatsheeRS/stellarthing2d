using frambos.core;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace frambos.graphics;

public static class Renderer {
    public static GL gl { get; set; }

    /// <summary>
    /// starts opengl
    /// </summary>
    public static void setup()
    {
        Frambos.log("setting up opengl");
        gl.ClearColor(0.1f, 0.5f, 0.9f, 1f);
    }
}