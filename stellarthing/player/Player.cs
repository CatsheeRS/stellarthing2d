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

    Sprite spr = load<Sprite>("bob_guy.png");

    TransformComp tf = new() {
        position = vec3(-500, 0, -500),
    };
    TileComp render = new();

    public Player() {}

    public void update(double delta)
    {
        //Camera.target = vec2(tf.position.x, tf.position.z);
        tf.position += vec3(100 * delta, 0, 100 * delta);
        tf.rotation += 750 * delta;
        render.update(spr, tf);
    }
}