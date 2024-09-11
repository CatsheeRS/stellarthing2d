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
        model.rlModel.Transform = Raymath.MatrixTranslate((float)-model.center.x, (float)-model.center.y,
            (float)-model.center.z);
        
        model.rlModel.Transform = Raymath.MatrixMultiply(model.rlModel.Transform, Raymath.MatrixRotateXYZ(
            new Vector3((float)(Raylib.DEG2RAD * tf.rotation.x), (float)(Raylib.DEG2RAD * tf.rotation.y),
            (float)(Raylib.DEG2RAD * tf.rotation.z))));
        
        Raylib.DrawModel(model.rlModel, new Vector3((float)tf.position.x, (float)tf.position.y,
            (float)tf.position.z), (float)tf.scale / settings.meterSize, Color.White);
    }
}