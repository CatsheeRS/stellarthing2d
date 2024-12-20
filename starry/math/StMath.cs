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
}