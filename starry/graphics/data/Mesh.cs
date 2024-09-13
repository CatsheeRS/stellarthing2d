using static starry.Starry;

namespace starry;

/// <summary>
/// raw model data
/// </summary>
public struct Mesh(Triangle[] verts) {
    public Triangle[] tris { get; set; } = verts;

    /// <summary>
    /// mesh data for a cube that's precisely 1 meter big
    /// </summary>
    public static Mesh cube {
        get => new([
            new Triangle(vec3(0, 0, 0), vec3(0, 1, 0), vec3(1, 1, 0)),
            new Triangle(vec3(0, 0, 0), vec3(1, 1, 0), vec3(1, 0, 0)),
            new Triangle(vec3(1, 0, 0), vec3(1, 1, 0), vec3(1, 1, 1)),
            new Triangle(vec3(1, 0, 0), vec3(1, 1, 1), vec3(1, 0, 1)),
        ]);
    }
}