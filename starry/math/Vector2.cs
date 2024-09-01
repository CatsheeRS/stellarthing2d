namespace starry;

/// <summary>
/// mostly just exists since i didn't know if i was supposed to use system.numerics vectors or silk.net vectors
/// </summary>
public struct Vector2(double x, double y) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
    public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);

    public static Vector2 zero { get => new(0, 0); }
}