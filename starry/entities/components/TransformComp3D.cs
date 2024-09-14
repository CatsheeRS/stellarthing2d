using static starry.Starry;

namespace starry;

/// <summary>
/// adds stuff for the isometric game world
/// </summary>
public class TransformComp3D {
    /// <summary>
    /// position in meters. it's important to note that Y is up, similar to opengl, raylib, godot, and minecraft
    /// </summary>
    public vec3 position { get; set; } = vec3();
    
    int isotation;
    /// <summary>
    /// isometric rotation, 0 is pointing west, 1 is pointing south, 2 is pointing east, and 3 is pointing north, see https://files.catbox.moe/lpu4ur.png (not malware). this will automatically loop if it's smaller than 0 or bigger than 3
    /// </summary>
    public int isoRotation {
        get { return isotation; }
        set {
            int val = value;
            if (val < 0) val = 0;
            if (val > 3) val = 3;
            isotation = val;
        }
    }

    /// <summary>
    /// a scale factor for the object. this is just multiplication so 1 is the original size, smaller values make it smaller, and bigger values make it bigger
    /// </summary>
    public vec2 scale { get; set; } = vec2(1, 1);
    /// <summary>
    /// origin point, each number must be from 0 to 1, (0, 0, 0) is top left, and (0.5, 0.5, 0.5) is the center
    /// </summary>
    public vec2 origin { get; set; } = vec2(0.5, 0.5);
}