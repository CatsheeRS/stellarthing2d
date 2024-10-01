using System;
using System.Text;
using Newtonsoft.Json;

namespace starry;

/// <summary>
/// main static class for the engine, recommended to be statically imported, as in <c>using static Starry;</c>
/// </summary>
public static class Starry {
    /// <summary>
    /// settings for the engine
    /// </summary>
    public static StarrySettings settings { get; set; }

    /// <summary>
    /// starts up the engine :D, see StarrySettings for more
    /// </summary>
    public static void create(StarrySettings settings)
    {
        Starry.settings = settings;

        log("Starting engine...");
        Application.create();
    }

    /// <summary>
    /// it's like Console.WriteLine() but it doesn't print garbage with any custom types or lists or dictionaries
    /// </summary>
    public static void log(params object[] x)
    {
        if (!settings.verbose) return;

        StringBuilder str = new();
        foreach (var item in x) {
            // we optimize common types so the game doesn't explode
            switch (item) {
                case string strr: str.Append(strr); break;
                case sbyte i8: str.Append(i8.ToString("N0")); break;
                case byte u8: str.Append(u8.ToString("N0")); break;
                case short i16: str.Append(i16.ToString("N0")); break;
                case ushort u16: str.Append(u16.ToString("N0")); break;
                case int i32: str.Append(i32.ToString("N0")); break;
                case uint u32: str.Append(u32.ToString("N0")); break;
                case long i64: str.Append(i64.ToString("N0")); break;
                case ulong u64: str.Append(u64.ToString("N0")); break;
                case float f32: str.Append(f32.ToString("N0")); break;
                case double f64: str.Append(f64.ToString("N0")); break;
                case decimal f128: str.Append(f128.ToString("N0")); break;
                case bool boo: str.Append(boo ? "true" : "false"); break;
                case vec2 vecthesecond: str.Append($"({vecthesecond.x}, {vecthesecond.y})"); break;
                case vec2i vecthesecondi: str.Append($"({vecthesecondi.x}, {vecthesecondi.y})"); break;
                case vec3 vecthethird: str.Append($"({vecthethird.x}, {vecthethird.y}, {vecthethird.z})"); break;
                case vec3i vecthethirdi: str.Append($"({vecthethirdi.x}, {vecthethirdi.y}, {vecthethirdi.z})"); break;
                case color col: str.Append($"rgba({col.r}, {col.g}, {col.b}, {col.a})"); break;
                case null: str.Append("null"); break;
                default: str.Append(JsonConvert.SerializeObject(item, Formatting.Indented)); break;
            }

            if (x.Length > 1) str.Append(", ");
        }
        str.Append('\n');
        Console.Write(str);
        DebugMode.text += str;
    }
    
    /// <summary>
    /// if true, the game is currently in debug mode
    /// </summary>
    public static bool isDebug()
    {
        #if DEBUG
        return true;
        #else
        return false;
        #endif
    }

    /// <summary>
    /// shortcut for new vec3()
    /// </summary>
    public static vec3 vec3(double x, double y, double z) => new(x, y, z);
    /// <summary>
    /// shortcut for new vec2()
    /// </summary>
    public static vec2 vec2(double x, double y) => new(x, y);
    /// <summary>
    /// shortcut for new vec3i()
    /// </summary>
    public static vec3i vec3i(int x, int y, int z) => new(x, y, z);
    /// <summary>
    /// shortcut for new vec2i()
    /// </summary>
    public static vec2i vec2i(int x, int y) => new(x, y);
    /// <summary>
    /// shortcut for new color()
    /// </summary>
    public static color color(byte r, byte g, byte b, byte a) => new(r, g, b, a);
    /// <summary>
    /// color() but without transparency
    /// </summary>
    public static color color(byte r, byte g, byte b) => new(r, g, b, 255);

    /// <summary>
    /// shortcut for vec3.zero
    /// </summary>
    public static vec3 vec3() => starry.vec3.zero;
    /// <summary>
    /// shortcut for vec2.zero
    /// </summary>
    public static vec2 vec2() => starry.vec2.zero;
    /// <summary>
    /// shortcut for vec3i.zero
    /// </summary>
    public static vec3i vec3i() => starry.vec3i.zero;
    /// <summary>
    /// shortcut for vec2i.zero
    /// </summary>
    public static vec2i vec2i() => starry.vec2i.zero;

    /// <summary>
    /// loads an asset, with the path specified in the project settings. for efficiency, assets are loaded once and then put into a dictionary with the paths. this is literally just Assets.load()
    /// </summary>
    public static T load<T>(string path) where T : IAsset, new()
    {
        return Assets.load<T>(path);
    }

    /// <summary>
    /// converts degrees to radians
    /// </summary>
    public static double deg2rad(double deg) => deg * (Math.PI / 180);
    /// <summary>
    /// converts radians to degrees
    /// </summary>
    public static double rad2deg(double rad) => rad * (180 / Math.PI);
}
