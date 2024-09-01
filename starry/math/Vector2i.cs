namespace starry;

/// <summary>
/// Vector2: integer edition
/// </summary>
public struct Vector2i(int x, int y) {
    public int x { get; set; } = x;
    public int y { get; set; } = y;

    public static Vector2i operator +(Vector2i a, Vector2i b) => new(a.x + b.x, a.y + b.y);
    public static Vector2i operator -(Vector2i a, Vector2i b) => new(a.x - b.x, a.y - b.y);
    public static Vector2i operator *(Vector2i a, Vector2i b) => new(a.x * b.x, a.y * b.y);
    public static Vector2i operator /(Vector2i a, Vector2i b) => new(a.x / b.x, a.y / b.y);

    public static Vector2i zero { get => new(0, 0); }
}