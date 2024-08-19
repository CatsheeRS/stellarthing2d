using System;
using System.Collections.Generic;
using Godot;

namespace stellarthing;

public static class Universe
{
    /// <summary>
    /// the current hour in the game, from 0 to 23
    /// </summary>
    public static int Hour {
        get => UniverseManager.GetProperty<int>("hour");
        set => UniverseManager.SetProperty("hour", value);
    }

    /// <summary>
    /// the current day in the game, from 1 to 30
    /// </summary>
    public static int Day {
        get => UniverseManager.GetProperty<int>("day");
        set => UniverseManager.SetProperty("day", value);
    }

    /// <summary>
    /// the current month in the game, from 1 to 12
    /// </summary>
    public static int Month {
        get => UniverseManager.GetProperty<int>("month");
        set => UniverseManager.SetProperty("month", value);
    }

    /// <summary>
    /// the current day in the game, starts at 2371
    /// </summary>
    public static int Year {
        get => UniverseManager.GetProperty<int>("year");
        set => UniverseManager.SetProperty("year", value);
    }

    public static Dictionary<string, Variant> DefaultValues { get; set; } = new() {
        { "hour", 12 },
        { "day", 15 },
        { "month", 2 },
        { "year", 2371 },
    };
}