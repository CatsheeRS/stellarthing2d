using System;
using starry;
using static starry.Starry;

namespace stellarthing;

public class SpaceScene : IEntity
{
    public EntityInformation setup() => new() {
        type = EntityType.gameWorld,
        tag = "space_scene",
    };

    Sprite spr = load<Sprite>("galaxy.png");

    TransformComp tf = new() {
        position = vec3(0, 0, 0),
        scale = vec2(2, 2),
    };
    TileComp render = new();

    public SpaceScene() {}

    public void update(double delta)
    {
        render.update(spr, tf);
    }
}