using System;
using starry;
using static starry.Starry;

namespace stellarthing;

public class Player : IEntity
{
    public EntityInformation setup() => new() {
        type = EntityType.gameWorld,
        tag = "player",
    };

    Model model = load<Model>("furniture/elfridge.vox");

    TransformComp3D tf = new() {
        position = vec3(0, 0, 0),
        origin = vec3(1, 1, 1),
    };
    ModelComp modelrender = new();

    public Player()
    {
        
    }

    public void update(double delta)
    {
        tf.rotation += vec3(0, 5, 0);
        modelrender.update(model, tf);
    }
}