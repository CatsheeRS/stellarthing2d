namespace starry;

/// <summary>
/// it's a tile
/// </summary>
public class TileComp(Sprite sprite) {
    /// <summary>
    /// it's a sprite
    /// </summary>
    public Sprite sprite { get; set; } = sprite;
    /// <summary>
    /// position, each unit is a tile, not a pixel. (0, 0) is the top left, this means positive X is right and positive Y is down
    /// </summary>
    public vec2 position { get; set; } = (0, 0);
    /// <summary>
    /// the origin point, from the top left, from 0 to 1, (0.5, 0.5) is the center
    /// </summary>
    public vec2 origin { get; set; } = (0.5, 0.5);
    /// <summary>
    /// rotation in degrees
    /// </summary>
    public double rotation { get; set; } = 0;
    /// <summary>
    /// scale (multiplier)
    /// </summary>
    public vec2 scale { get; set; } = (1, 1);
    /// <summary>
    /// the tint of the tile (white uses the default colors)
    /// </summary>
    public color tint { get; set; } = color.white;
}