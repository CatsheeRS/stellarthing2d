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

    public void update(double delta)
    {
        if (Input.isKeymapJustPressed("explode")) log("A Grande Greguificação");
        //log(Input.mousePosition);
        if (Input.isMouseButtonPressed(MouseButton.left)) log("La Gran Greguificación");
        log(vec2(69, 420) == vec2(69, 420));
    }
}