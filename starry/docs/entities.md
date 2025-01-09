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
- Always running

Game world entities never runs in paused mode, while managers and UI can run in pause mode so there is any way for the user to unpause stuff.

Always running entities are similar to game world entities, but they aren't affected by [the chunk system](tilemap.md#chunks), so their update functions are still ran no matter how far players are from it. Convenient for industries, as forcing players to fit their factories in a single chunk would be very stupid.

There's groups.

Doing group stuff is probably fast since it's hash based and stuff.

Also entities are async against your will.

See the `Entities` class

## Components

Components are important if you want your entities to like, do anything.

Any class that implements `IComponent` can be used with entities:

```cs
// add it to this entity
var comp = Entities.addComponent<Component>(ent2ref(this));

// get or add components from other entities
var comp = Entities.getComponent<Component>(entref);
```

You can make your own components too:

```csharp
public class MyComponent: IComponent {
    public void create(IEntity entity) {}
    public void update(IEntity entity, double delta) {}
    public void draw(IEntity entity) {}
}
```

## Entity references

Sometimes entities need to reference other entities, however JSON doesn't support that, so it would just repeat the entities over and over again, which is bad since sending 5 TB of data to other players takes a while.

To solve that we have entity references, which are random 10 character long base64 strings, which should be enough for 1.152×10¹⁸ (around 1 quintillion) entities.

The convention is that entity references should be named `entref` or `somethingRef`, to make it clear that it's not some random string, it's actually a reference.

Once you have a reference you can get the actual entity with `Starry.ref2ent()`.

If you need a reference but don't have it, use `Starry.ent2ref()`.

## Metadata

Entities can have metadata too. They're just key-value pairs.

To set metadata just use `Entities.setMeta()`

To get metadata you can use `Entities.getMeta<T>()`, which allows setting a default value if the value doesn't exist yet.