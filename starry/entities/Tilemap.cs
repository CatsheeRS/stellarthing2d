using System;
using System.Collections.Generic;
using Raylib_cs;
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
    static Dictionary<string, Dictionary<int, Queue<(Sprite, TransformComp)>>> worldLayerSprites = [];
    internal static Camera2D rlcam;

    internal static void create()
    {
        rlcam = new Camera2D {
            Target = new Vector2(0, 0),
            Offset = new Vector2(settings.renderSize.x / 2f,
                                 settings.renderSize.y / 2f),
            Rotation = 0,
            Zoom = 1,
        };
    }

    /// <summary>
    /// adds a sprite to the world, intended to be used by TileComp. you have to run this every frame as the renderer is gonna pop everything in the update loop. the world by default is "space"
    /// </summary>
    public static void pushSprite(string world, int layer, Sprite sprite, TransformComp tf)
    {
        if (!worldLayerSprites.ContainsKey(world)) worldLayerSprites.Add(world, []);
        if (!worldLayerSprites[world].ContainsKey(layer)) worldLayerSprites[world].Add(layer, []);
        worldLayerSprites[world][layer].Enqueue((sprite, tf));
    }

    internal static void update()
    {
        //log(rlcam.Offset.X, rlcam.Offset.Y);
        while (worldLayerSprites[world][layer].Count > 0) {
            var sprtf = worldLayerSprites[world][layer].Dequeue();
            // slightly complicated lmao
            Raylib.DrawTexturePro(
                sprtf.Item1.rlSprite,
                new Rectangle(0, 0, sprtf.Item1.size.x, sprtf.Item1.size.y),
                new Rectangle((float)sprtf.Item2.position.x - -(float)(sprtf.Item1.size.x * sprtf.Item2.scale.x / 2),
                    (float)sprtf.Item2.position.z - -(float)(sprtf.Item1.size.y * sprtf.Item2.scale.y / 2),
                    (float)(sprtf.Item1.size.x * sprtf.Item2.scale.x),
                    (float)(sprtf.Item1.size.y * sprtf.Item2.scale.y)),
                new Vector2((float)(sprtf.Item1.size.x * sprtf.Item2.scale.x) / 2,
                            (float)(sprtf.Item1.size.y * sprtf.Item2.scale.y) / 2),
                (float)sprtf.Item2.rotation,
                new Color(sprtf.Item2.tint.r, sprtf.Item2.tint.g, sprtf.Item2.tint.b, sprtf.Item2.tint.a)
            );
        }
    }

    /// <summary>
    /// annihilates an entire world from existence
    /// </summary>
    public static void cleanupWorld(string world)
    {
        worldLayerSprites.Remove(world);
    }
}

/// <summary>
/// the camera :) this is actually just a nice wrapper around Tilemap.rlcam
/// </summary>
public static class Camera
{
    /// <summary>
    /// the target of the camera
    /// </summary>
    public static vec2 target {
        get => vec2(Tilemap.rlcam.Target.X, Tilemap.rlcam.Target.Y);
        set => Tilemap.rlcam.Target = new Vector2((float)value.x, (float)value.y);
    }
    /// <summary>
    /// an offset applied to the camera's target
    /// </summary>
    public static vec2 offset {
        get => vec2(Tilemap.rlcam.Offset.X, Tilemap.rlcam.Offset.Y) -
                    vec2(settings.renderSize.x / 2f,
                         settings.renderSize.y / 2f);

        set => Tilemap.rlcam.Offset = new Vector2((float)value.x, (float)value.y) +
                    new Vector2(settings.renderSize.x * Renderer.scaleFactor / 2f,
                                settings.renderSize.y * Renderer.scaleFactor / 2f);
    }
    /// <summary>
    /// camera rotation in degrees
    /// </summary>
    public static double rotation {
        get => Tilemap.rlcam.Rotation;
        set => Tilemap.rlcam.Rotation = (float)value;
    }
    /// <summary>
    /// camera zoom. this is multiplication so 1 is the default
    /// </summary>
    public static double zoom {
        get => Tilemap.rlcam.Zoom;
        set => Tilemap.rlcam.Zoom = (float)value;
    }
}