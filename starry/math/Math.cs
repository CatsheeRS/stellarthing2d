using System;

namespace starry;

/// <summary>
/// now its time for everybodys favorite subject.. MATH answer the three questions correctly and you might get something special
/// </summary>
public class Math
{
    /// <summary>
    /// degree to radian
    /// </summary>
    public static double deg2rad(double deg) => deg * (System.Math.PI / 180);
    
    /// <summary>
    /// radian to degree
    /// </summary>
    public static double rad2deg(double rad) => rad * (180 / System.Math.PI);
}