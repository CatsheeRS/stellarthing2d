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

    //Sprite spr = load<Sprite>("galaxy.png");

    TransformComp3D tf = new() {
        position = vec3(0, 0, 0),
    };

    public SpaceScene() {}

    //public void draw() => render.draw(spr, tf);
}