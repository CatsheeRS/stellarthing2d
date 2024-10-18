using System;
using System.Collections.Generic;
using static starry.Starry;

namespace starry;

public struct StarrySettings
{
    public StarrySettings() {}

    /// <summary>
    /// ran when the game starts
    /// </summary>
    public required Action startup { get; set; }
    /// <summary>
    /// the game name, used for the window title and stuff
    /// </summary>
    public string gameName { get; set; } = "Starry Engine";
    /// <summary>
    /// the game's version. should start with a lowercase V.
    /// </summary>
    public string gameVersion { get; set; } = "v0.0.0";
    /// <summary>
    /// if true, the engine will display all logging, which is probably bad for performance since json
    /// </summary>
    public bool verbose { get; set; } = false;
    /// <summary>
    /// keyboard actions that can be remapped
    /// </summary>
    public Dictionary<string, Key[]> keymap { get; set; } = [];
    /// <summary>
    /// place where assets are loaded, usually relative to the project folder
    /// </summary>
    public string assetPath { get; set; } = "";
    /// <summary>
    /// resolution in which the game is rendered.
    /// </summary>
    public vec2i renderSize { get; set; } = vec2i(192, 108);
    /// <summary>
    /// path to the atlas image. an atlas contains all of the image
    /// </summary>
    public string atlas { get; set; } = "";
    /// <summary>
    /// all of the sprites from the atlas image. the key is a name for the sprite, and the value is a rectangle inside the atlas (position followed by size, in pixels)
    /// </summary>
    public Dictionary<string, (vec2i, vec2i)> sprites { get; set; } = [];
}
