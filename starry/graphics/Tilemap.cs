using System;
using System.Collections.Generic;
using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the epic world, not to be confused with World which manages entities
/// </summary>
public static class Tilemap {
    /// <summary>
    /// the current world. by default it's space
    /// </summary>
    public static string world { get; set; } = "";
    /// <summary>
    /// the current layer of the world. the surface is 0
    /// </summary>
    public static int layer { get; set; } = 0;

    /// <summary>
    /// as the name implies, it's a bunch of sprites for the layers of the worlds. this type declaration is a mess, so first there's a dictionary of worlds, then there's the layers, which is a dictionary since there can be an indefinite amount of positive and negative indexes, then there's a queue instead of a list since it's faster, and finally there's a tuple of a sprite and a transform component
    /// </summary>
    //static Dictionary<string, Dictionary<int, Queue<(Sprite, TransformComp3D)>>> worldLayerSprites = [];

    internal static void create()
    {
        Camera.offset -= settings.renderSize / vec2i(2, 2);
    }

    /// <summary>
    /// adds a sprite to the world, intended to be used by TileComp. you have to run this every frame as the renderer is gonna pop everything in the update loop. the world by default is "space"
    /// </summary>
    /*public static void pushSprite(string world, int layer, Sprite sprite, TransformComp3D tf)
    {
        if (!worldLayerSprites.ContainsKey(world)) worldLayerSprites.Add(world, []);
        if (!worldLayerSprites[world].ContainsKey(layer)) worldLayerSprites[world].Add(layer, []);
        worldLayerSprites[world][layer].Enqueue((sprite, tf));
    }*/

    internal static void update()
    {
        /*while (worldLayerSprites[world][layer].Count > 0) {
            var sprtf = worldLayerSprites[world][layer].Dequeue();
            var killme = vec2i(
                            (int)Math.Round(sprtf.Item1.size.x * sprtf.Item2.scale.x),
                            (int)Math.Round(sprtf.Item1.size.y * sprtf.Item2.scale.y))
                            * vec2(Camera.zoom, Camera.zoom);
            
            // slightly complicated lmao
            Platform.renderTexture(
                sprtf.Item1,
                vec2i(), sprtf.Item1.size,
                vec2i(
                    (int)Math.Round(sprtf.Item2.position.x - -(sprtf.Item1.size.x * sprtf.Item2.scale.x / 2)),
                    (int)Math.Round(sprtf.Item2.position.z - -(sprtf.Item1.size.y * sprtf.Item2.scale.y / 2))
                ) + Camera.target + Camera.offset,
                vec2i((int)Math.Round(killme.x), (int)Math.Round(killme.y)),
                sprtf.Item2.rotation + Camera.rotation,
                vec2i(
                    (int)Math.Round(sprtf.Item1.size.x * sprtf.Item2.scale.x) / 2,
                    (int)Math.Round(sprtf.Item1.size.y * sprtf.Item2.scale.y) / 2
                ),
                color(sprtf.Item2.tint.r, sprtf.Item2.tint.g, sprtf.Item2.tint.b, sprtf.Item2.tint.a)
            );
        }*/
    }

    /// <summary>
    /// annihilates an entire world from existence
    /// </summary>
    public static void cleanupWorld(string world)
    {
        //worldLayerSprites.Remove(world);
    }
}

/// <summary>
/// the camera for the tilemap world thing :D
/// </summary>
public static class Camera
{
    /// <summary>
    /// the target of the camera
    /// </summary>
    public static vec2i target { get; set; } = vec2i();
    /// <summary>
    /// an offset applied to the camera's target
    /// </summary>
    public static vec2i offset { get; set; } = vec2i();
    /// <summary>
    /// camera rotation in degrees
    /// </summary>
    public static double rotation { get; set; } = 0;
    /// <summary>
    /// camera zoom. this is multiplication so 1 is the default
    /// </summary>
    public static double zoom { get; set; } = 1;
}