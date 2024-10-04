# starry engine

handsome game engine

## modules

- core/Application: this is where the main loop is ran and the engine both starts and ends
- core/Save: flabbergasting save system
- core/assets: everything assets
- core/input: simple input system
- entities: the entity system, as well as `Tilemap` which manages the tile rendering
- entities/components: default components you can use and stuff
- graphics: graphics stuff :D
- math: some vectors and the color struct
- platform: cross-platform abstractions for the rest of the engine (currently only supports SDL2 through [these bindings](https://github.com/ppy/SDL2-CS))
- Starry: utility functions, intended to be used with `using static starry.Starry;`

## funni conventions

- everything should be `camelCase` except type names which should be `PascalCase`
- braces should be on the same line except for functions which should use c# conventions
- constructors and destructors for static classes should be called `create()` and `cleanup()` respectively, and functions ran in the main loop should be called `update()`
- speaking of which, you should use these wherever possible
    - static classes for everything that doesn't need to be instanced multiple times or any manager
    - uint instead of int
    - `Queue<T>`/`Stack<T>` instead of `List<T>` (it's faster, specially important if you're running stuff every frame)
- everything should pass through the platform module before getting to whatever framework the engine is currently using, except for the game which shouldn't have to interact with the platform module at all
- the game can only run code through entities and the `StarrySettings.startup` function