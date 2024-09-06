namespace starry;

/// <summary>
/// use for making shader stuff :D
/// </summary>
public interface IShader {
    public string getSource();
    public ShaderType getType();
}

/// <summary>
/// type of shader
/// </summary>
public enum ShaderType
{
    /// <summary>
    /// does stuff with positions and stuff
    /// </summary>
    vertex,
    /// <summary>
    /// does stuff with colors
    /// </summary>
    fragment,
}