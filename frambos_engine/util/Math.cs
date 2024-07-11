namespace frambos.util;

/// <summary>
/// mostly just exists since i didn't know if i was supposed to use system.numerics vectors or silk.net vectors
/// </summary>
public struct Vector2(double x, double y) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;
    public static Vector2 zero { get => new(0, 0); }
}

/// <summary>
/// it's like vector2 but + 1
/// </summary>
public struct Vector3(double x, double y, double z) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;
    public double z { get; set; } = z;
}