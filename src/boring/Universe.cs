using System;
using System.Collections.Generic;
using Godot;

namespace stellarthing;

public static class Universe
{
    public static int Hour {
        get => UniverseManager.GetProperty<int>("hour");
        set => UniverseManager.SetProperty("hour", value);
    }
    public static int Day {
        get => UniverseManager.GetProperty<int>("day");
        set => UniverseManager.SetProperty("day", value);
    }
    public static int Month {
        get => UniverseManager.GetProperty<int>("month");
        set => UniverseManager.SetProperty("month", value);
    }
    public static int Year {
        get => UniverseManager.GetProperty<int>("year");
        set => UniverseManager.SetProperty("year", value);
    }

    public static Dictionary<string, object> DefaultValues { get; set; } = new() {
        { "hour", 12 },
        { "day", 15 },
        { "month", 2 },
        { "year", 2371 },
    };
}