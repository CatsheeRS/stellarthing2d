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
        // raylib is interesting
        model.rlModel.Transform = Raymath.MatrixRotateXYZ(new System.Numerics.Vector3((float)(Raylib.DEG2RAD *
            tf.rotation.x), (float)(Raylib.DEG2RAD * tf.rotation.y), (float)(Raylib.DEG2RAD * tf.rotation.z)));
        
        Raylib.DrawModel(model.rlModel, new System.Numerics.Vector3((float)tf.position.x, (float)tf.position.y,
            (float)tf.position.z), (float)tf.scale, Color.White);
    }
}