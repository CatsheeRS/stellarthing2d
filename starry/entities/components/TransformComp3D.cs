using static starry.Starry;

namespace starry;

/// <summary>
/// adds 3d stuff
/// </summary>
public class TransformComp3D {
    /// <summary>
    /// position in meters. it's important to note that Y is up, similar to opengl, raylib, godot, and minecraft
    /// </summary>
    public vec3 position { get; set; } = vec3();
    /// <summary>
    /// the rotation, in degrees. X is pitch, Y is yaw, and Z is roll, see https://en.wikipedia.org/wiki/Aircraft_principal_axes
    /// </summary>
    public vec3 rotation { get; set; } = vec3();
    /// <summary>
    /// a scale factor for the object. this is just multiplication so 1 is the original size, smaller values make it smaller, and bigger values make it bigger
    /// </summary>
    public vec3 scale { get; set; } = vec3(1, 1, 1);
    /// <summary>
    /// origin point, each number must be from 0 to 1, (0, 0, 0) is bottom left, and (0.5, 0.5, 0.5) is the center
    /// </summary>
    public vec3 origin { get; set; } = vec3();
}