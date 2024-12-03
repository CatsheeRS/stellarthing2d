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
        /*Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(color.r, color.g, color.b, color.a));*/
    }

    public static void endDrawing()
    {
        //Raylib.EndDrawing();
    }

    public static void cleanup() {}
}