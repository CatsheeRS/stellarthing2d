# Entities

Entities are cool.

To make an entity you can implement the IEntity interface. (only `getEntityType()` is required)

```cs
public class Example : IEntity {
    public EntityType entityType => EntityType.gameWorld;
    public string name => "Example©®™©™®©®™©™";
    public string[] initGroups => [];

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

There's groups.

Doing group stuff is probably fast since it's hash based and stuff.

Also entities are async against your will.

See the `Entities` class

## Components

Components are important if you want your entities to like, do anything.

Any class that implements `IComponent` can be used with entities:

```cs
// add it to this entity
var comp = Entities.addComponent<Component>(this);

// get or add components from other entities
var comp = Entities.getComponent<Component>(entity);
```

You can make your own components too:

```csharp
public class MyComponent: IComponent {
    public void create(IEntity entity) {}
    public void update(IEntity entity, double delta) {}
    public void draw(IEntity entity) {}
}
```