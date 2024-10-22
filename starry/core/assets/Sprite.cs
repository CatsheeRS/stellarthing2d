using System;
using System.IO;
using StbImageSharp;
using static starry.Starry;
namespace starry;

/// <summary>
/// it's a sprite
/// </summary>
public class Sprite : IAsset {
    /// <summary>
    /// the size of the texture, in pixels
    /// </summary>
    public vec2i size { get; internal set; }
    /// <summary>
    /// pixel data for the pixels. you can edit this property to edit sprites
    /// </summary>
    public color[,] data { get; set; } = new color[0, 0];

    public void load(string path) {
        using var stream = File.OpenRead(path);
        ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        data = new color[image.Width, image.Height];
        size = vec2i(image.Width, image.Height);

        int eye = 0;
        for (int y = 0; y < image.Height; y++) {
            for (int x = 0; x < image.Height; x++) {
                byte r = image.Data[eye++];
                byte g = image.Data[eye++];
                byte b = image.Data[eye++];
                byte a = image.Data[eye++];
                data[x, y] = color(r, g, b, a);
            }
        }
    }

    // this sprite shit isn't based on sdl, the garbage collector will catch it
    public void cleanup() {}
}