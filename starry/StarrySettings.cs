using System;

namespace starry;

/// <summary>
/// defines game engine settings
/// </summary>
public struct StarrySettings {
    /// <summary>
    /// function ran when the game starts
    /// </summary>
    public required Action startup { get; set; }
}