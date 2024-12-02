using System.Numerics;
using Raylib_cs;
namespace starry;

public static partial class Graphics {
    public static Font defaultFont { get; set; } = new();

    public static void drawText(string text, Font font, vec2 pos, color color, int fontSize)
    {
        Raylib.DrawTextEx(font.rl, text, new Vector2((float)pos.x, (float)pos.y), fontSize, 0,
            new Color(color.r, color.g, color.b, color.a));
    }
}