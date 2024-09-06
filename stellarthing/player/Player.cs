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
    public TestComp test { get; set; } = new();

    public void update(double delta)
    {
        if (Input.isKeyPressed(Key.pause)) log("fuck off");
    }

    /*public bool input(IInputEvent @event)
    {
        if (@event.getType() == InputType.keypress) {
            var lol = (KeypressEvent)@event;
            log(lol.key.ToString(), lol.type.ToString(), "is explode action: ", lol.isKeymap("explode"));
            return true;
        }
        return false;
    }*/
}