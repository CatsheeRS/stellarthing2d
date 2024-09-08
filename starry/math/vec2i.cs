using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// Vector2: integer edition
/// </summary>
public struct vec2i(int x, int y) {
    public int x { get; set; } = x;
    public int y { get; set; } = y;

    public static vec2i operator +(vec2i a, vec2i b) => new(a.x + b.x, a.y + b.y);
    public static vec2i operator -(vec2i a, vec2i b) => new(a.x - b.x, a.y - b.y);
    public static vec2i operator *(vec2i a, vec2i b) => new(a.x * b.x, a.y * b.y);
    public static vec2i operator /(vec2i a, vec2i b) => new(a.x / b.x, a.y / b.y);
    public static bool operator >(vec2i a, vec2i b) => a.x > b.x && a.x > b.x;
    public static bool operator <(vec2i a, vec2i b) => a.x < b.x && a.x < b.x;
    public static bool operator >=(vec2i a, vec2i b) => a.x >= b.x && a.x >= b.x;
    public static bool operator <=(vec2i a, vec2i b) => a.x <= b.x && a.x <= b.x;
    public static implicit operator vec2(vec2i a) => new(a.x, a.y);

    public static vec2i zero { get => new(0, 0); }

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
}