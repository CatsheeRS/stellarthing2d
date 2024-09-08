using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// it's like vector3 but + 1
/// </summary>
public struct vec3(double x, double y, double z) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;
    public double z { get; set; } = z;

    public static vec3 operator +(vec3 a, vec3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static vec3 operator -(vec3 a, vec3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static vec3 operator *(vec3 a, vec3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static vec3 operator /(vec3 a, vec3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public static bool operator >(vec3 a, vec3 b) => a.x > b.x && a.x > b.x && a.z > b.z;
    public static bool operator <(vec3 a, vec3 b) => a.x < b.x && a.x < b.x && a.z < b.z;
    public static bool operator >=(vec3 a, vec3 b) => a.x >= b.x && a.x >= b.x && a.z >= b.z;
    public static bool operator <=(vec3 a, vec3 b) => a.x <= b.x && a.x <= b.x && a.z <= b.z;
    
    public static vec3 zero { get => new(0, 0, 0); }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is vec3 vec) {
            return vec.x == x && vec.y == y && vec.z == z;
        }
        return false;
    }

    public static bool operator ==(vec3 a, vec3 b) => a.Equals(b);
    public static bool operator !=(vec3 a, vec3 b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(x);
        hash.Add(y);
        hash.Add(z);
        return hash.ToHashCode();
    }
}