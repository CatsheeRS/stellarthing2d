using System.IO;
using System.Linq;
using starry;
using stellarthing;
using static starry.Starry;

create(new StarrySettings {
    startup = () => {
        World.addEntity(new Player());
    },
    gameName = "Stellarthing",
    gameVersion = "v0.8.0",
    verbose = args.Contains("--verbose") || isDebug(),
    keymap = {
        {"explode", [Key.space]},
    },
    assetPath = Path.GetFullPath("assets"),
    renderSize = vec2i(1920, 1080),
    tileSize = vec2i(96, 96),
});