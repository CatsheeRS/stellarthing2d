using System;

namespace starry;

/// <summary>
/// defines game engine settings
/// </summary>
public struct StarrySettings() {
    /// <summary>
    /// function ran when the game starts
    /// </summary>
    public required Action startup { get; set; }
    public string gameName { get; set; } = "Starry Project";
    public vec3i gameVersion { get; set; } = (0, 0, 1);
    public bool showVersion { get; set; } = true;
    public bool fullscreen { get; set; } = true;
    public vec2i renderSize { get; set; } = (200, 100);
    public bool antiAliasing { get; set; } = true;
    public string assetPath { get; set; } = "";
    public int frameRate { get; set; } = 60;
    public bool verbose { get; set; } = false;
    /// <summary>
    /// the size of the tiles
    /// </summary>
    public vec2i tileSize { get; set; } = (0, 0);
}