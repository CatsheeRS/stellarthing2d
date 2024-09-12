using System;
using System.Diagnostics.CodeAnalysis;

namespace starry;

/// <summary>
/// it's a color, every component is from 0 to 255
/// </summary>
public struct @color(byte r, byte g, byte b, byte a) {
    /// <summary>
    /// red
    /// </summary>
    public byte r { get; set; } = r;
    /// <summary>
    /// green
    /// </summary>
    public byte g { get; set; } = g;
    /// <summary>
    /// blue
    /// </summary>
    public byte b { get; set; } = b;
    /// <summary>
    /// alpha (transparency)
    /// </summary>
    public byte a { get; set; } = a;
    
    // colors :D
    public static color white { get => new(255, 255, 255, 255); }
    public static color black { get => new(0, 0, 0, 255); }
    public static color transparent { get => new(0, 0, 0, 0); }
    public static color lightGray { get => new(200, 200, 200, 255); }
    public static color gray { get => new(130, 130, 130, 255); }
    public static color darkGray { get => new(80, 80, 80, 255); }
    public static color yellow { get => new(255, 249, 0, 255); }
    public static color gold { get => new(255, 203, 0, 255); }
    public static color orange { get => new(255, 161, 0, 255); }
    public static color pink { get => new(255, 109, 194, 255); }
    public static color red { get => new(230, 41, 55, 255); }
    public static color maroon { get => new(190, 33, 55, 255); }
    public static color green { get => new(0, 228, 48, 255); }
    public static color lime { get => new(0, 158, 47, 255); }
    public static color darkGreen { get => new(0, 117, 44, 255); }
    public static color skyBlue { get => new(102, 191, 255, 255); }
    public static color blue { get => new(0, 121, 241, 255); }
    public static color darkBlue { get => new(0, 82, 172, 255); }
    public static color purple { get => new(200, 122, 255, 255); }
    public static color violet { get => new(135, 60, 190, 255); }
    public static color darkPurple { get => new(112, 32, 126, 255); }
    public static color beige { get => new(211, 176, 131, 255); }
    public static color brown { get => new(127, 106, 79, 255); }
    public static color darkBrown { get => new(76, 63, 47, 255); }
    public static color magenta { get => new(255, 0, 255, 255); }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is color col) {
            return col.r == r && col.g == g && col.b == b && col.a == a;
        }
        return false;
    }

    public static bool operator ==(color a, color b) => a.Equals(b);
    public static bool operator !=(color a, color b) => !(a == b);

    public override readonly int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(r);
        hash.Add(g);
        hash.Add(b);
        hash.Add(a);
        return hash.ToHashCode();
    }
}