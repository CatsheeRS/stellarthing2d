using System;
using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds a tile to your entity
/// </summary>
public class TileComp {
    /// <summary>
    /// run in your update function
    /// </summary>
    public void update(Sprite sprite, TransformComp tf)
    {
        Tilemap.pushSprite(Tilemap.world, (int)Math.Round(tf.position.y), sprite, tf);
    }
}