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
    public GraphicsTestComp test { get; set; } = new();

    public void update(double delta)
    {
        test.update();
    }

    public bool input(IInputEvent @event)
    {
        return false;
    }
}