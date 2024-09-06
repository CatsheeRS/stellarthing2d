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
    }

    public bool input(IInputEvent @event)
    {
        return false;
    }
}