using static starry.Starry;

namespace starry;

/// <summary>
/// adds a 3d 
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
    /// a scale factor for the object, 1 means the original size
    /// </summary>
    public double scale { get; set; } = 1;
}