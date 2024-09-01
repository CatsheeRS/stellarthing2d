using starry;
using static starry.Starry;

start(new StarrySettings {
    gameVersion = "v0.8.0",
    verbose = args.Contains("--verbose") || isDebug(),
});