using System;
using System.Collections.Generic;

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
}