using System;
// help
using static starry.Starry;
using static SDL2.SDL;
using SDL2;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it
/// </summary>
public static partial class Platform {
    internal static color[,]? atlasdata;
    internal static Dictionary<string, color[,]> help = [];

    internal static void loadAtlas(string path)
    {
        // wtf is this
        using Image img = Image.Load(path);
        var data = img.CloneAs<Rgba32>();
        atlasdata = new color[img.Width, img.Height];

        // i fucking hate the programming world send help
        // why can´t they just make a reasonable library
        int i = 0;
        Queue<Queue<color>> steitjgi = [];
        data.ProcessPixelRows(x => {
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
                atlasdata[x, y] = lol.Dequeue();
                x++;
            }
            y++;
        }

        splitAtlas();
        log(help);
    }

    internal static void splitAtlas()
    {
        foreach (var fhish in settings.sprites) {
            color[,] ithoughroughlyenjoythisdoughnought = splitAtlasButSmaller(fhish.Value.Item1, fhish.Value.Item2);
            help.Add(fhish.Key, ithoughroughlyenjoythisdoughnought);
        }
    }

    internal static color[,] splitAtlasButSmaller(vec2i pos, vec2i size)
    {
        color[,] fuckoff = new color[size.x, size.y];
        for (int y = pos.y; y < size.y + pos.y; y++) {
            for (int x = pos.x; x < size.x + pos.x; x++) {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                fuckoff[x - pos.x, y - pos.y] = atlasdata[x, y];
                log(atlasdata[x, y]);
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
        return fuckoff;
    }
}