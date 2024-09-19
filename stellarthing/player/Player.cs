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
        position = vec3(0, 0, 0),
    };
    TileComp render = new();

    public Player() {}

    public void update(double delta)
    {
        Camera.target = vec2(tf.position.x, tf.position.y);
        tf.rotation += 1;
        render.update(spr, tf);
    }
}