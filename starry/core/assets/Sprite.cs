using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
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
    /// pixel data for the pixels
    /// </summary>
    public color[,] data { get; set; } = new color[0, 0];

    public void load(string path) {
        // wtf is this
        using Image img = Image.Load(path);
        var imgdata = img.CloneAs<Rgba32>();
        data = new color[img.Width, img.Height];

        // i fucking hate the programming world send help
        // why can´t they just make a reasonable library
        int i = 0;
        Queue<Queue<color>> steitjgi = [];
        imgdata.ProcessPixelRows(x => {
            Span<Rgba32> h = x.GetRowSpan(i);
            Queue<color> faffery = [];
            foreach (var j in h) {
                faffery.Enqueue(color(j.R, j.G, j.B, j.A));
            }
            steitjgi.Enqueue(faffery);
            i++;
        });

        // murders of murderers
        int y = 0;
        while (steitjgi.Count > 0) {
            var lol = steitjgi.Dequeue();
            int x = 0;
            while (lol.Count > 0) {
                data[x, y] = lol.Dequeue();
                x++;
            }
            y++;
        }

        
    }

    // this sprite shit isn't based on sdl, the garbage collector will catch it
    public void cleanup() {}
}