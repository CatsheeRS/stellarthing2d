using System;
using System.Diagnostics.CodeAnalysis;
namespace starry;

/// <summary>
/// rect
/// </summary>
public struct rect2(double x, double y, double w, double h) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;
    public double w { get; set; } = w;
    public double h { get; set; } = h;
    public readonly vec2 pos => (x, y);
    public readonly vec2 size => (w, h);

    public static implicit operator rect2((double, double, double, double) m) =>
        new(m.Item1, m.Item2, m.Item3, m.Item4);
    public static implicit operator rect2((vec2, vec2) m) =>
        new(m.Item1.x, m.Item1.y, m.Item2.x, m.Item2.y);

    public static rect2 zero { get => new(0, 0, 0, 0); }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is rect2 r) {
            return r.x == x && r.y == y && r.w == w && r.h == r.h;
        }
        return false;
    }

    public static bool operator ==(rect2 a, rect2 b) => a.Equals(b);
    public static bool operator !=(rect2 a, rect2 b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(x);
        hash.Add(y);
        hash.Add(w);
        hash.Add(h);
        return hash.ToHashCode();
    }

    // for using rect in switch statements :D
    public readonly void Deconstruct(out double ecks, out double why, out double doubleyou, out double eich)
    {
        ecks = x;
        why = y;
        doubleyou = w;
        eich = h;
    }

    public override readonly string ToString() => $"rect({x}, {y}, {w}, {h})";
}