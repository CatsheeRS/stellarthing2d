﻿using System.IO;
using frambos;
using frambos.core;
using frambos.ecs;
using frambos.util;

MainLoop.setup(args, () => {
    AssetManager.respath = Path.GetFullPath("assets");

    // setup game
    Entity lamo = new("test");
    lamo.add_system<Sprite>();
    lamo.get_comp<SpriteTexture>().texture = Frambos.load<Texture>("ben.png");
    lamo.get_comp<Transform>().size = new Vector2(1280, 720);
});