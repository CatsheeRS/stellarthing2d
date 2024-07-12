using frambos.core;
using frambos.util;

namespace frambos.ecs;

/// <summary>
/// basic component for all things visible
/// </summary>
public class Transform : IComponent
{
    /// <summary>
    /// 0, 0 is the top left corner, x is horizontal, y is vertical, z is used for the layer system
    /// </summary>
    public Vector2 position { get; set; } = Vector2.zero;
    /// <summary>
    /// size in pixels
    /// </summary>
    public Vector2 size { get; set; } = Vector2.zero;
    /// <summary>
    /// rotation in degrees
    /// </summary>
    public double rotation { get; set; } = 0;
    /// <summary>
    /// used for rotation, divide size by half to make it the center
    /// </summary>
    public Vector2 center { get; set; } = Vector2.zero;
    /// <summary>
    /// if true, the entity will be rendered
    /// </summary>
    public bool visible { get; set; } = true;

    public string get_key() => "transform";
}

/// <summary>
/// component used with the Sprite system
/// </summary>
public class SpriteTexture : IComponent
{
    #nullable enable
    public Texture? texture { get; set; } = null;
    #nullable disable
    public bool flip_x { get; set; } = false;
    public bool flip_y { get; set; } = false;

    public string get_key() => "sprite_texture";
}