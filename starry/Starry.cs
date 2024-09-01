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
            if (item is string str) Console.WriteLine(str);
            else Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
        }
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
    /// shortcut for new Vector3()
    /// </summary>
    public static Vector3 vec3(double x, double y, double z) => new(x, y, z);
    /// <summary>
    /// shortcut for new Vector2()
    /// </summary>
    public static Vector2 vec2(double x, double y) => new(x, y);
    /// <summary>
    /// shortcut for new Vector3i()
    /// </summary>
    public static Vector3i vec3i(int x, int y, int z) => new(x, y, z);
    /// <summary>
    /// shortcut for new Vector2i()
    /// </summary>
    public static Vector2i vec2i(int x, int y) => new(x, y);

    /// <summary>
    /// shortcut for Vector3.zero
    /// </summary>
    public static Vector3 vec3() => Vector3.zero;
    /// <summary>
    /// shortcut for Vector2.zero
    /// </summary>
    public static Vector2 vec2() => Vector2.zero;
    /// <summary>
    /// shortcut for Vector3i.zero
    /// </summary>
    public static Vector3i vec3i() => Vector3i.zero;
    /// <summary>
    /// shortcut for Vector2i.zero
    /// </summary>
    public static Vector2i vec2i() => Vector2i.zero;
}
