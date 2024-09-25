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
    renderSize = vec2i(1920, 1080),
    tileSize = vec2i(96, 96),
});