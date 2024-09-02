using System;
using static starry.Starry;

namespace starry;

public struct EntityInformation {
    public EntityInformation() {}

    /// <summary>
    /// type of the entity; affects rendering, input, and pausing. update functions are called in this order: pausable/paused manager -> pausable/pause ui -> game world
    /// </summary>
    public required EntityType type { get; set; }
    /// <summary>
    /// debug name for the entity
    /// </summary>
    public string tag { get; set; } = "";
    /// <summary>
    /// groups the entity is assigned to when created
    /// </summary>
    public string[] groups { get; set; } = [];
}

/// <summary>
/// type of the entity; affects rendering, input, and pausing. update functions are called in this order: pausable/paused manager -> pausable/pause ui -> game world
/// </summary>
public enum EntityType {
    /// <summary>
    /// pausable 3d object
    /// </summary>
    gameWorld,
    /// <summary>
    /// pausable 2d interface
    /// </summary>
    ui,
    /// <summary>
    /// 2d interface only ran when paused
    /// </summary>
    pauseUi,
    /// <summary>
    /// entity that isn't rendered or interacted with (doesn't receive input) and just manages stuff, can be paused
    /// </summary>
    pausableManager,
    /// <summary>
    /// entity that isn't rendered or interacted with (doesn't receive input) and just manages stuff, only rendered on pause
    /// </summary>
    pausedManager,
}