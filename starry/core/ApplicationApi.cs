using System;
using Raylib_cs;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static partial class Application {
    /// <summary>
    /// time in seconds since the engine has started up
    /// </summary>
    /// <returns></returns>
    public static double getTime() => Raylib.GetTime();
}