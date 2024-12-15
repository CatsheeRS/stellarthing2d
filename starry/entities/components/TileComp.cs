using System;
namespace starry;

/// <summary>
/// it's a tile
/// </summary>
public class TileComp {
    public TileComp(TileSprite spr) {
        tileSprite = spr;
    }

    public TileComp(Sprite spr) {
        tileSprite = new() {
            left = spr,
            right = spr,
            top = spr,
            bottom = spr,
            size = spr.size,
        };
    }

    /// <summary>
    /// it's a sprite
    /// </summary>
    public TileSprite tileSprite { get; set; }
    /// <summary>
    /// intended for when you want to change sprites manually and shit also animation or smth idfk anymore
    /// </summary>
    public Sprite sprite {
        get => tileSprite.left;
        set {
            tileSprite = new() {
                left = value,
                right = value,
                top = value,
                bottom = value,
                size = value.size,
            };
        }
    }
    /// <summary>
    /// position, each unit is a tile, not a pixel. (0, 0) is the top left, this means positive X is right and positive Y is down. Z is the layers, on a range of -128-512
    /// </summary>
    public vec3 position { get; set; } = (0, 0, 0);
    /// <summary>
    /// the world the tile is located in. by default this is space or whatever
    /// </summary>
    public string world { get; set; } = "";
    /// <summary>
    /// the origin point, from the top left, from 0 to 1, (0.5, 0.5) is the center
    /// </summary>
    public vec2 origin { get; set; } = (0.5, 0.5);
    /// <summary>
    /// rotation in degrees
    /// </summary>
    public double rotation { get; set; } = 0;
    /// <summary>
    /// sprites can have several sides because why not. each side's sprite filename must end with the side's starting letter. (l, r, t, b)
    /// </summary>
    public TileSide side { get; set; } = TileSide.left;
    /// <summary>
    /// scale (multiplier)
    /// </summary>
    public vec2 scale { get; set; } = (1, 1);
    /// <summary>
    /// the tint of the tile (white uses the default colors)
    /// </summary>
    public color tint { get; set; } = color.white;
}

/// <summary>
/// sprites can have several sides because why not. each side's sprite filename must end with the side's starting letter. (l, r, t, b)
/// </summary>
public enum TileSide {
    left,
    right,
    top,
    bottom,
}

public static class TileSideExtensions {
    /// <summary>
    /// rotates the tile by 90 degrees
    /// </summary>
    public static TileSide rotateClockwise(this TileSide tile) 
    {
        return tile switch {
            TileSide.left => TileSide.top,
            TileSide.right => TileSide.bottom,
            TileSide.top => TileSide.right,
            TileSide.bottom => TileSide.left,
            _ => throw new Exception("you fucking moron"),
        };
    }

    /// <summary>
    /// rotates the tile by -90 degrees
    /// </summary>
    public static TileSide rotateCounterClockwise(this TileSide tile) 
    {
        return tile switch {
            TileSide.left => TileSide.bottom,
            TileSide.right => TileSide.top,
            TileSide.top => TileSide.left,
            TileSide.bottom => TileSide.right,
            _ => throw new Exception("you fucking moron"),
        };
    }
}