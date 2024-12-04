using System;
using SimulationFramework;
using SimulationFramework.Drawing;
namespace starry;

public static partial class Graphics {
    internal static ICanvas? canvas;

    public static void create(ICanvas canvas)
    {
        Graphics.canvas = canvas;
    }

    public static void clear(color color)
    {
        canvas?.Clear(new Color(color.r, color.g, color.b, color.a));
    }
    
    public static void drawSprite(Sprite sprite, rect2 rect, double rotation, color color)
    {
        canvas?.DrawTexture()
    }
}