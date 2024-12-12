# Tilemap

Tilemaps are quite cool.

Tile coordinates are vector3s because the Z axis is the layer, which makes it kinda 3D.

Tile coordinates also are in number of tiles, not pixels.

There's also support for multiple worlds, "" is space.

Layers range from -128 to 512:
- -128 to -1: underground stuff
- 0: the ground
- 1 to 512: just space for more building

See the `Tilemap` class.