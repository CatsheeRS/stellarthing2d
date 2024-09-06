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
        log(Input.mousePosition);
    }

    public bool input(IInputEvent @event)
    {
        if (@event.getType() == InputType.mouseButton) {
            var elmierda = (MouseButtonEvent)@event;
            log(elmierda.button, elmierda.state);
            return true;
        }
        return false;
    }
}