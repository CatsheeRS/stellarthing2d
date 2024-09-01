using Newtonsoft.Json;

namespace starry;

/// <summary>
/// main static class for the engine, recommended to be statically imported, as in <c>using static Starry;</c>
/// </summary>
public static class Starry {
    static StarrySettings settings;

    /// <summary>
    /// starts up the engine :D, see StarrySettings for more
    /// </summary>
    public static void start(StarrySettings settings)
    {
        Starry.settings = settings;

        log("Starting engine...");

        // test stuff
        log(settings);
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
        bool r = false;
        #if DEBUG
        r = true;
        #endif
        return r;
    }
}
