using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds a light to the 3d world
/// </summary>
public class LightComp {
    /// <summary>
    /// if true, the light is emitting light
    /// </summary>
    public bool enabled { get; set; } = true;
    /// <summary>
    /// the position of the light
    /// </summary>
    public vec3 position { get; set; } = vec3();
    /// <summary>
    /// the color of the light
    /// </summary>
    public color color { get; set; } = color.white;
    /// <summary>
    /// the energy of the light
    /// </summary>
    public double energy { get; set; } = 1;

    public LightComp() {}

    public void update() {}
}