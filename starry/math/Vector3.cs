namespace starry;

/// <summary>
/// it's like vector2 but + 1
/// </summary>
public struct Vector3(double x, double y, double z) {
    public double x { get; set; } = x;
    public double y { get; set; } = y;
    public double z { get; set; } = z;

    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Vector3 operator *(Vector3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3 operator /(Vector3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    
    public static Vector3 zero { get => new(0, 0, 0); }
}