using frambos.core;
using frambos.util;

namespace frambos.ecs;

/// <summary>
/// basic component for all things visible
/// </summary>
public struct Transform : IComponent
{
    /// <summary>
    /// 0, 0 is the top left corner, x is horizontal, y is vertical, z is used for the layer system
    /// </summary>
    public Vector3 position { get; set; }
    /// <summary>
    /// size in pixels
    /// </summary>
    public Vector2 size { get; set; }
    /// <summary>
    /// rotation in degrees
    /// </summary>
    public double rotation { get; set; }
    /// <summary>
    /// used for rotation, divide size by half to make it the center
    /// </summary>
    public Vector2 center { get; set; }
    /// <summary>
    /// if true, the entity will be rendered
    /// </summary>
    public bool visible { get; set; }

    public IComponent new_with_defaults()
    {
        return new Transform {
            position = Vector3.zero,
            size = Vector2.zero,
            rotation = 0,
            center = Vector2.zero,
            visible = true
        };
    }
}

/// <summary>
/// component used with the Sprite system
/// </summary>
public struct SpriteTexture : IComponent
{
    #nullable enable
    public Texture? texture { get; set; }
    #nullable disable
    public bool flip_x { get; set; }
    public bool flip_y { get; set; }

    public IComponent new_with_defaults()
    {
        return new SpriteTexture {
            texture = null,
            flip_x = false,
            flip_y = false
        };
    }
}