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
}