using System.IO;
using frambos.core;
using frambos.ecs;
using spacegame.player;

MainLoop.setup(args, () => {
    AssetManager.respath = Path.GetFullPath("assets");

    // setup game
    Entity lamo = new("player");
    lamo.add_system<Player>();
});