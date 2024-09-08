using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// mostly just exists since i didn't know if i was supposed to use system.numerics vectors or silk.net vectors
/// </summary>
public struct vec2(double x, double y) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;

    public static vec2 operator +(vec2 a, vec2 b) => new(a.x + b.x, a.y + b.y);
    public static vec2 operator -(vec2 a, vec2 b) => new(a.x - b.x, a.y - b.y);
    public static vec2 operator *(vec2 a, vec2 b) => new(a.x * b.x, a.y * b.y);
    public static vec2 operator /(vec2 a, vec2 b) => new(a.x / b.x, a.y / b.y);
    public static bool operator >(vec2 a, vec2 b) => a.x > b.x && a.x > b.x;
    public static bool operator <(vec2 a, vec2 b) => a.x < b.x && a.x < b.x;
    public static bool operator >=(vec2 a, vec2 b) => a.x >= b.x && a.x >= b.x;
    public static bool operator <=(vec2 a, vec2 b) => a.x <= b.x && a.x <= b.x;

    public static vec2 zero { get => new(0, 0); }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is vec2 vec) {
            return vec.x == x && vec.y == y;
        }
        return false;
    }

    public static bool operator ==(vec2 a, vec2 b) => a.Equals(b);
    public static bool operator !=(vec2 a, vec2 b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(x);
        hash.Add(y);
        return hash.ToHashCode();
    }
}