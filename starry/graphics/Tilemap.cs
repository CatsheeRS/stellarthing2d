using System;
using System.Collections.Concurrent;
namespace starry;

/// <summary>
/// A tile-based video game, or grid-based video game, is a type of video game where the playing area consists of small square (or, much less often, rectangular, parallelogram, or hexagonal) graphic images referred to as tiles laid out in a grid. That the screen is made of such tiles is a technical distinction, and may not be obvious to people playing the game. The complete set of tiles available for use in a playing area is called a tileset. Tile-based games usually simulate a top-down, side view, or 2.5D view of the playing area, and are almost always two-dimensional. 
/// </summary>
public static class Tilemap {
    /// <summary>
    /// the last underground layer
    /// </summary>
    public const int MIN_LAYER = -128;
    /// <summary>
    /// the last overworld layer
    /// </summary>
    public const int MAX_LAYER = 512;

    /// <summary>
    /// dictionary of worlds, list of layers, queue of tiles
    /// </summary>
    internal static ConcurrentDictionary<string, ConcurrentDictionary<int, ConcurrentQueue<TileComp>>> worlds = new();

    /// <summary>
    /// the current layers of each world
    /// </summary>
    public static ConcurrentDictionary<string, int> currentLayers { get; set; }= new();
    /// <summary>
    /// the current world. "" is space
    /// </summary>
    public static string currentWorld { get; set; } = "";
    /// <summary>
    /// the position of the camera (in tile coordinates)
    /// </summary>
    public static vec2 camPosition { get; set; } = (0, 0);
    /// <summary>
    /// the camera offset (in pixels)
    /// </summary>
    public static vec2 camOffset { get; set; } = Starry.settings.renderSize / (2, 2);
    /// <summary>
    /// the camera scale (multiplier)
    /// </summary>
    public static vec2 camScale { get; set; } = (1, 1);

    public static void create()
    {
        createWorld("");
    }

    /// <summary>
    /// adds a tile to the stuff
    /// </summary>
    public static void pushTile(TileComp tile)
    {
        // yesterday i went outside with my mama's mason jar caught a lovely butterfly when i woke up today looked in on my fairy pet she had withered all away no more sighing in her breast i'm sorry for what i did i did what my body told me to i didn't mean to do you harm every time i pin down what i think i want it slips away the ghost slips away smell you on my hand for days i can't wash away your scent if i'm a dog then you're a bitch i guess you're as real as me maybe i can live with that maybe i need fantasy life of chasing butterfly i'm sorry for what i did i did what my body told me to i didn't mean to do you harm every time i pin down what i think i want it slips away the ghost slips away i told you i would return when the robin makes his nest but i ain't never coming back i'm sorry i'm sorry i'm sorry
        worlds[tile.world][(int)Math.Round(tile.position.z)].Enqueue(tile);
    }

    /// <summary>
    /// creates a new world
    /// </summary>
    public static void createWorld(string name)
    {
        ConcurrentDictionary<int, ConcurrentQueue<TileComp>> world = new();
        for (int i = MIN_LAYER; i < MAX_LAYER; i++) {
            world.TryAdd(i, []);
        }
        worlds.TryAdd(name, world);
        currentLayers.TryAdd(name, 0);
    }

    public static void update()
    {
        ConcurrentQueue<TileComp> bloodyTiles = worlds[currentWorld][currentLayers[currentWorld]];

        // hell
        while (!bloodyTiles.IsEmpty) {
            bloodyTiles.TryDequeue(out TileComp? tile);
            if (tile == null) continue;

            Sprite sprite = tile.side switch {
                TileSide.left => tile.sprite.left,
                TileSide.right => tile.sprite.right,
                TileSide.top => tile.sprite.top,
                TileSide.bottom => tile.sprite.bottom,
                _ => throw new Exception("shut up marge shut up"),
            };

            Graphics.drawSprite(
                sprite,
                ((tile.position.as2d() * Starry.settings.tileSize) + camPosition + camOffset,
                tile.sprite.size * tile.scale * camScale),
                tile.origin,
                tile.rotation,
                tile.tint
            );
        }
    }
}