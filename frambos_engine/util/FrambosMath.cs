using System;

namespace frambos.util;

/// <summary>
/// does math stuffs :)
/// </summary>
public static class FrambosMath {
    /// <summary>
    /// converts radians to degrees
    /// </summary>
    public static double rad2deg(double rad) => rad * (180 / Math.PI);
    /// <summary>
    /// converts degrees to radians
    /// </summary>
    public static double deg2rad(double deg) => deg * (Math.PI / 180);

    /// <summary>
    /// returns a rotation (in degrees) that looks like it's pointing towards the target position
    /// </summary>
    public static double look_at(Vector2 pos, Vector2 target)
    {
        Vector2 dir = target - pos;
        double angle = Math.Atan2(dir.y, dir.x);
        return rad2deg(angle);
    }
}