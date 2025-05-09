using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// Vector2: integer edition
/// </summary>
public struct vec2i(long x, long y) {
    public long x { get; set; } = x;
    public long y { get; set; } = y;

    public static implicit operator vec2i((long, long) m) => new(m.Item1, m.Item2);
    public static vec2i operator +(vec2i a, vec2i b) => new(a.x + b.x, a.y + b.y);
    public static vec2i operator -(vec2i a, vec2i b) => new(a.x - b.x, a.y - b.y);
    public static vec2i operator *(vec2i a, vec2i b) => new(a.x * b.x, a.y * b.y);
    public static vec2i operator /(vec2i a, vec2i b) => new(a.x / b.x, a.y / b.y);
    public static vec2i operator %(vec2i a, vec2i b) => new(a.x % b.x, a.y % b.y);
    public static bool operator >(vec2i a, vec2i b) => a.x > b.x && a.x > b.x;
    public static bool operator <(vec2i a, vec2i b) => a.x < b.x && a.x < b.x;
    public static bool operator >=(vec2i a, vec2i b) => a.x >= b.x && a.x >= b.x;
    public static bool operator <=(vec2i a, vec2i b) => a.x <= b.x && a.x <= b.x;
    public static vec2i operator -(vec2i a) => new(-a.x, -a.y);
    public static implicit operator vec2(vec2i a) => new(a.x, a.y);

    public static vec2i zero => new(0, 0);

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is vec2i vec) {
            return vec.x == x && vec.y == y;
        }
        return false;
    }

    public static bool operator ==(vec2i a, vec2i b) => a.Equals(b);
    public static bool operator !=(vec2i a, vec2i b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(x);
        hash.Add(y);
        return hash.ToHashCode();
    }

    // for using vec2i in switch statements :D
    public readonly void Deconstruct(out long ecks, out long why)
    {
        ecks = x;
        why = y;
    }

    public override readonly string ToString() => $"vec2i({x}, {y})";
}