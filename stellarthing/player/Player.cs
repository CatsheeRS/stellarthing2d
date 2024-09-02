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
        test.update();
    }
}