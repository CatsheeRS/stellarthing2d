namespace starry;

/// <summary>
/// it's like vector2 but + 1
/// </summary>
public struct Vector3i(int x, int y, int z) {
    public int x { get; set; } = x;
    public int y { get; set; } = y;
    public int z { get; set; } = z;

    public static Vector3i operator +(Vector3i a, Vector3i b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3i operator -(Vector3i a, Vector3i b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Vector3i operator *(Vector3i a, Vector3i b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3i operator /(Vector3i a, Vector3i b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    
    public static Vector3i zero { get => new(0, 0, 0); }
}