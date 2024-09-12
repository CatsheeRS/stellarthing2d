using System.Numerics;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// it's a 3d model
/// </summary>
public class Model : IAsset {
    /// <summary>
    /// the center of the model
    /// </summary>
    public vec3 center { get; set; } = vec3();
    /// <summary>
    /// the top right part of the model
    /// </summary>
    public vec3 topRight { get; set; } = vec3();
    internal Raylib_cs.Model rlModel;

    public void load(string path) => rlModel = Raylib.LoadModel(path);
    public void cleanup() => Raylib.UnloadModel(rlModel);
}