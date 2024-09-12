using System.Numerics;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds a 3d model to your entity
/// </summary>
public class ModelComp {
    /// <summary>
    /// renders a model, run in your update function
    /// </summary>
    public void update(Model model, TransformComp3D tf)
    {
        // i love meth i mean math
        model.rlModel.Transform = Raymath.MatrixIdentity();

        BoundingBox bb = Raylib.GetModelBoundingBox(model.rlModel);

        Vector3 elOrigen = new(bb.Min.X + ((bb.Max.X - bb.Min.X) * (float)tf.origin.x),
                             bb.Min.Y + ((bb.Max.Y - bb.Min.Y) * (float)tf.origin.y),
                             bb.Min.Z + ((bb.Max.Z - bb.Min.Z) * (float)tf.origin.z));
        model.rlModel.Transform = Raymath.MatrixTranslate((float)-elOrigen.X, (float)-elOrigen.Y,
            (float)-elOrigen.Z);
        
        model.rlModel.Transform = Raymath.MatrixMultiply(model.rlModel.Transform, Raymath.MatrixRotateXYZ(
            new Vector3((float)(Raylib.DEG2RAD * tf.rotation.x), (float)(Raylib.DEG2RAD * tf.rotation.y),
            (float)(Raylib.DEG2RAD * tf.rotation.z))));
        
        Raylib.DrawModel(model.rlModel, new Vector3((float)tf.position.x, (float)tf.position.y,
            (float)tf.position.z), (float)tf.scale / settings.meterSize, Color.White);
    }
}