using static starry.Starry;

namespace starry;

/// <summary>
/// adds stuff
/// </summary>
public class TransformComp3D {
    /// <summary>
    /// position, with 1 being 1 tile, (0, 0, 0) being the top left, and the Y part being the layer it's located, with 0 being the surface, and on the planets you have to be between -64 and 1024
    /// </summary>
    public vec3 position { get; set; } = vec3();
    /// <summary>
    /// the position converted to non-tile coordinates
    /// </summary>
    public vec2 globalPosition {
        get => position.as2d() * settings.tileSize;
        set => (value / settings.tileSize).as3d(position.y);
    }
    /// <summary>
    /// the rotation, in degrees
    /// </summary>
    //public double rotation { get; set; } = 0;
    /// <summary>
    /// the scale. this is just multiplication so (1, 1) is the original size, smaller values make it smaller, and bigger values make it bigger
    /// </summary>
    public vec2 scale { get; set; } = vec2(1, 1);
    /// <summary>
    /// tints the sprite (white is the original colors)
    /// </summary>
    //public color tint { get; set; } = color.white;
}