# Tilemap

Tilemaps are quite cool.

Tile coordinates are vector3s because the Z axis is the layer, which makes it kinda 3D.

Tile coordinates also are in number of tiles, not pixels.

There's also support for multiple worlds, "" is space.

Layers range from -128 to 512:
- -128 to -1: underground stuff
- 0: the ground
- 1 to 512: just space for more building

Tiles can have multiple sides for some reason.

To use tiles in your epic entities you can use the `Tile` class

You can just use the [component](entities.md#components)

```cs
var tile = Entities.addComponent<Tile>(this);
tile.sprite = new TileSprite(
    await load<Sprite>("left"),
    await load<Sprite>("right"),
    await load<Sprite>("top"),
    await load<Sprite>("bottom"),
);
```

Or make a new tile and manually make it render

```csharp
tile = new Tile();
tile.sprite = new TileSprite(
    await load<Sprite>("left"),
    await load<Sprite>("right"),
    await load<Sprite>("top"),
    await load<Sprite>("bottom"),
);

// put this in your draw function
Tilemap.pushTile(tile);
```

You can also use [animations](animation.md) with `Tile`

See the `Tilemap` class.