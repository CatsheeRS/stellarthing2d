using System.Threading.Tasks;
using Raylib_cs;

namespace starry;

public static class Graphics {
    public static Task create()
    {
        return Task.CompletedTask;
    }

    public static void clear(color color)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(color.r, color.g, color.b, color.a));
    }

    public static void endDrawing()
    {
        Raylib.EndDrawing();
    }

    public static Task cleanup()
    {
        return Task.CompletedTask;
    }
}