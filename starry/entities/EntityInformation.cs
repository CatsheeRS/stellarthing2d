using System;
using static starry.Starry;

namespace starry;

public struct EntityInformation {
    public EntityInformation() {}

    public string tag { get; set; } = "";
    public ProcessModes processMode { get; set; } = ProcessModes.pausable;
}

/// <summary>
/// how entities react to pausing
/// </summary>
public enum ProcessModes {
    /// <summary>
    /// stops processing when the world is paused
    /// </summary>
    pausable,
    /// <summary>
    /// processes only when the world is paused
    /// </summary>
    whenPaused,
    /// <summary>
    /// always processes, ignoring pausing
    /// </summary>
    always,
    /// <summary>
    /// never processes, ignoring pausing
    /// </summary>
    never
}