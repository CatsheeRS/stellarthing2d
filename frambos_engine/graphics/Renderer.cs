using frambos.core;
using Silk.NET.OpenGL;

namespace frambos.graphics;

public static class Renderer {
    public static GL gl { get; set; }

    public static void on_load()
    {
        setup_opengl();
    }

    public static void on_update(double delta)
    {
        Frambos.log("updating");
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static void on_render(double delta)
    {
        Frambos.log("rendering");
    }

    public static void setup_opengl()
    {
        Frambos.log("creating opengl");
        gl = MainLoop.window.CreateOpenGL();
        gl.ClearColor(0.1f, 0.5f, 0.9f, 1f);
    }
}