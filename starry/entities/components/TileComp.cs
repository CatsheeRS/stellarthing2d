using System;
using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds a tile to your entity
/// </summary>
public class TileComp {
    /// <summary>
    /// run in your draw function
    /// </summary>
    public void draw(Sprite sprite, TransformComp3D tf)
    {
        Tilemap.pushSprite(Tilemap.world, (int)Math.Round(tf.position.y), sprite, tf);
    }
}