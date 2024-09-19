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

    Sprite spr = load<Sprite>("cheese.png");

    WorldTransformComp tf = new() {
        position = vec3(0, 0, 0),
        origin = vec2(0.5, 1),
    };
    WorldSpriteComp render = new();

    public Player() {}

    public void update(double delta)
    {
        render.update(spr, tf);
    }
}