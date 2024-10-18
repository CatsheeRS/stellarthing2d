using System.IO;
using System.Linq;
using starry;
using stellarthing;
using static starry.Starry;

create(new StarrySettings {
    startup = () => {
        Tilemap.world = "space";
        World.addEntity(new SpaceScene());
        World.addEntity(new Player());
    },
    gameName = "Stellarthing",
    gameVersion = "v0.9.0",
    verbose = args.Contains("--verbose") || isDebug(),
    keymap = {
        {"move_up", [Key.w, Key.arrowUp]},
        {"move_left", [Key.a, Key.arrowLeft]},
        {"move_down", [Key.s, Key.arrowDown]},
        {"move_right", [Key.d, Key.arrowRight]},
    },
    assetPath = Path.GetFullPath("assets"),
    renderSize = vec2i(240, 160),
    atlas = "atlas.png",
    sprites = {
        {"red_circle",      (vec2i(16 * 0, 16 * 0), vec2i(16, 16))},
        {"green_circle",    (vec2i(16 * 1, 16 * 1), vec2i(16, 16))},
        {"blue_circle",     (vec2i(16 * 2, 16 * 2), vec2i(16, 16))},
    }
});
