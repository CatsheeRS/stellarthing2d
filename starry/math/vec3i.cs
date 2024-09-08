using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// it's like vector3 but + 1
/// </summary>
public struct vec3i(int x, int y, int z) {
    public int x { get; set; } = x;
    public int y { get; set; } = y;
    public int z { get; set; } = z;

    public static vec3i operator +(vec3i a, vec3i b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static vec3i operator -(vec3i a, vec3i b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static vec3i operator *(vec3i a, vec3i b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static vec3i operator /(vec3i a, vec3i b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public static bool operator >(vec3i a, vec3i b) => a.x > b.x && a.x > b.x && a.z > b.z;
    public static bool operator <(vec3i a, vec3i b) => a.x < b.x && a.x < b.x && a.z < b.z;
    public static bool operator >=(vec3i a, vec3i b) => a.x >= b.x && a.x >= b.x && a.z >= b.z;
    public static bool operator <=(vec3i a, vec3i b) => a.x <= b.x && a.x <= b.x && a.z <= b.z;

    public static vec3i zero { get => new(0, 0, 0); }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is vec3i vec) {
            return vec.x == x && vec.y == y && vec.z == z;
        }
        return false;
    }

    public static bool operator ==(vec3i a, vec3i b) => a.Equals(b);
    public static bool operator !=(vec3i a, vec3i b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(x);
        hash.Add(y);
        hash.Add(z);
        return hash.ToHashCode();
    }
}