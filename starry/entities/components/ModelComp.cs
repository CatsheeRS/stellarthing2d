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
        // for now we're just testing the renderer
        Renderer.drawMesh(Mesh.cube);
    }
}