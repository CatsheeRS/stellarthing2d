using static starry.Starry;

namespace starry;

/// <summary>
/// it's a triangle
/// </summary>
public struct Triangle(vec3 v1, vec3 v2, vec3 v3) {
    public vec3 vert1 { get; set; } = v1;
    public vec3 vert2 { get; set; } = v2;
    public vec3 vert3 { get; set; } = v3;
}