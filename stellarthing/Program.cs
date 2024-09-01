using System.Linq;
using starry;
using static starry.Starry;

create(new StarrySettings {
    gameName = "Stellarthing",
    gameVersion = "v0.8.0",
    verbose = args.Contains("--verbose") || isDebug(),
});