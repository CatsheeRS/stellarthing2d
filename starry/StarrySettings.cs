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
    public string gameName { get; set; } = "Example project";
    public vec3i gameVersion { get; set; } = (0, 0, 1);
    public bool fullscreen { get; set; } = true;
    public vec2i renderSize { get; set; } = (682, 384);
    public string assetPath { get; set; } = "";
    public int frameRate { get; set; } = 60;
    public bool verbose { get; set; } = false;
}