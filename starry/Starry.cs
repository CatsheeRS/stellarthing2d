using System;
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

        foreach (var item in x) {
            // we optimize common types so the game doesn't explode
            switch (item) {
                case string str: Console.Write(str); break;
                case sbyte i8: Console.Write(i8.ToString("N0")); break;
                case byte u8: Console.Write(u8.ToString("N0")); break;
                case short i16: Console.Write(i16.ToString("N0")); break;
                case ushort u16: Console.Write(u16.ToString("N0")); break;
                case int i32: Console.Write(i32.ToString("N0")); break;
                case uint u32: Console.Write(u32.ToString("N0")); break;
                case long i64: Console.Write(i64.ToString("N0")); break;
                case ulong u64: Console.Write(u64.ToString("N0")); break;
                case float f32: Console.Write(f32.ToString("N0")); break;
                case double f64: Console.Write(f64.ToString("N0")); break;
                case decimal f128: Console.Write(f128.ToString("N0")); break;
                case bool boo: Console.Write(boo ? "true" : "false"); break;
                case vec2 vecthesecond: Console.Write($"({vecthesecond.x}, {vecthesecond.y})"); break;
                case vec2i vecthesecondi: Console.Write($"({vecthesecondi.x}, {vecthesecondi.y})"); break;
                case vec3 vecthethird: Console.Write($"({vecthethird.x}, {vecthethird.y}, {vecthethird.z})"); break;
                case vec3i vecthethirdi: Console.Write($"({vecthethirdi.x}, {vecthethirdi.y}, {vecthethirdi.z})"); break;
                case null: Console.Write("null"); break;
                default: Console.Write(JsonConvert.SerializeObject(item, Formatting.Indented)); break;
            }

            if (x.Length > 1) Console.Write(", ");
        }
        Console.WriteLine();
    }
    
    // if true, the game is currently in debug mode
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
}
