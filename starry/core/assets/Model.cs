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
    internal Raylib_cs.Model rlModel;

    public unsafe void load(string path) {
        rlModel = Raylib.LoadModel(path);

        // center model
        BoundingBox bb = Raylib.GetModelBoundingBox(rlModel);
        Vector3 center = new(bb.Min.X + ((bb.Max.X - bb.Min.X) / 2),
                             bb.Min.Y + ((bb.Max.Y - bb.Min.Y) / 2),
                             bb.Min.Z + ((bb.Max.Z - bb.Min.Z) / 2));
        this.center = vec3(center.X, center.Y, center.Z);
        
        // traducíon is translation in spanish
        Matrix4x4 tradución = Raymath.MatrixTranslate(-center.X, -center.Y, -center.Z);
        rlModel.Transform = tradución;
    }

    public void cleanup() => Raylib.UnloadModel(rlModel);
}