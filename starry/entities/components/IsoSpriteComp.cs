using System.Numerics;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds a top down sprite to your entity
/// </summary>
public class WorldSpriteComp {
    /// <summary>
    /// run in your update function. it's important to note that the "sprites" parameter must be a path to the sprite, but to work properly you must save 4 images matching the isometric rotations (0-3, see TransformComp3D.isoRotation), but don't include the number in the path you provide
    /// </summary>
    public void update(string sprites, WorldTransformComp tf) {}
}