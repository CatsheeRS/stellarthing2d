using System;
namespace starry;

/// <summary>
/// now its time for everybodys favorite subject.. MATH answer the three questions correctly and you might get something special
/// </summary>
public static class StMath
{
    static Random sorandom = new();

    /// <summary>
    /// degree to radian
    /// </summary>
    public static double deg2rad(double deg) => deg * (Math.PI / 180);
    
    /// <summary>
    /// radian to degree
    /// </summary>
    public static double rad2deg(double rad) => rad * (180 / Math.PI);

    /// <summary>
    /// returns a random integer greater/equal to min and less than max
    /// </summary>
    public static long randint(long min, long max) => sorandom.NextInt64(min, max);

    /// <summary>
    /// returns a random float greater/equal to min and less than max
    /// </summary>
    public static double randfloat(double min, double max) =>
        sorandom.NextDouble() * (max - min) + min;
    
    /// <summary>
    /// returns a random vec2 greater/equal to min and less than max
    /// </summary>
    public static vec2 randvec2(vec2 min, vec2 max) =>
        (sorandom.NextDouble() * (max.x - min.x) + min.x,
         sorandom.NextDouble() * (max.y - min.y) + min.y);
    
    /// <summary>
    /// lerp (time is between 0 and 1)
    /// </summary>
    public static double lerp(double start, double end, double time) =>
        start + (end - start) * Math.Clamp(time, 0, 1);
    
    /// <summary>
    /// lerp (time is between 0 and 1)
    /// </summary>
    public static vec2 lerp(vec2 start, vec2 end, double time) =>
        (start.x + (end.x - start.x) * Math.Clamp(time, 0, 1),
         start.y + (end.y - start.y) * Math.Clamp(time, 0, 1));
    
    /// <summary>
    /// lerp (time is between 0 and 1)
    /// </summary>
    public static color lerp(color start, color end, double time) =>
        ((byte)(start.r + (end.r - start.r) * Math.Clamp(time, 0, 1)),
         (byte)(start.g + (end.g - start.g) * Math.Clamp(time, 0, 1)),
         (byte)(start.b + (end.b - start.b) * Math.Clamp(time, 0, 1)),
         (byte)(start.a + (end.a - start.a) * Math.Clamp(time, 0, 1)));
}