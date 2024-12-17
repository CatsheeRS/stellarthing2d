# Entities

Entities are cool.

To make an entity you can implement the IEntity interface. (only `getEntityType()` is required)

```cs
public class Example : IEntity {
    public EntityType getEntityType() => EntityType.gameWorld;
    public string getName() => "Example©®™©™®©®™©™";
    public string[] getInitGroups() => [];

    public void create() {}
    public void update(double delta) {}
    public void draw() {}
}
```

Entities have quite a few types ran in this order:

- Managers
- UI
- Game world

Game world entities never runs in paused mode, while managers and UI can run in pause mode so there is any way for the user to unpause stuff.

Also entities are async against your will