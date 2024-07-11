
using System;
using System.IO;
using frambos;
using frambos.core;

MainLoop.setup(args, () => {
    Frambos.log("starting");

    AssetManager.respath = Path.GetFullPath("./assets/");
});