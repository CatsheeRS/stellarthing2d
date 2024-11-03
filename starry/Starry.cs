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
    /// it's like Console.WriteLine() but it doesn't print garbage with any custom types or lists or dictionaries. keep in mind this might shit itself if it's running every frame in debug mode
    /// </summary>
    public static void log(params object[] x)
    {
        if (!settings.verbose) return;

        StringBuilder str = new();
        foreach (var item in x) {
            // we optimize common types so the game doesn't explode
            switch (item) {
                case string:
                case sbyte:
                case byte:
                case short:
                case ushort:
                case int:
                case uint:
                case long:
                case ulong:
                case float:
                case double:
                case decimal:
                case bool:
                    str.Append(item.ToString());
                    break;
                
                case vec2 wec2: str.Append($"({wec2.x}, {wec2.y})"); break;
                case vec2i wec2i: str.Append($"({wec2i.x}, {wec2i.y})"); break;
                case vec3 wec3: str.Append($"({wec3.x}, {wec3.y}, {wec3.z})"); break;
                case vec3i wec3i: str.Append($"({wec3i.x}, {wec3i.y}, {wec3i.z})"); break;
                case color coughlour: str.Append($"rgba({coughlour.r}, {coughlour.g}, {coughlour.b}, {coughlour.a})"); break;
                case null: str.Append("null"); break;
                // todo use custom fucking serializer
                default: str.Append(JsonConvert.SerializeObject(item, Formatting.Indented)); break;
            }

            if (x.Length > 1) str.Append(", ");
        }
        str.Append('\n');
        Console.Write(str);
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
