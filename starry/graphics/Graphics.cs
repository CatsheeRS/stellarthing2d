using System.Threading.Tasks;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
namespace starry;

public static partial class Graphics {
    internal static GL? gl;

    public static unsafe void create()
    {
        // shut up
        if (Window.glfw == null) return;

        gl = GL.GetApi(Window.glfw.GetProcAddress);
        Window.glfw.GetWindowSize(Window.window, out int wx, out int wy);
        gl.Viewport(0, 0, (uint)wx, (uint)wy);

        Starry.log("OpenGL has been initialized");
    }

    public static void clear(color color)
    {
        if (gl == null) return;
        gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static unsafe void endDrawing()
    {
        Window.glfw?.SwapBuffers(Window.window);
    }

    public static void cleanup() {}
}