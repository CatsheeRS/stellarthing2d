using System;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
namespace starry;

public static class Starry {
    /// <summary>
    /// starry settings
    /// </summary>
    public static StarrySettings settings { get; set; }
    /// <summary>
    /// the engine version (semantic versioning)
    /// </summary>
    public static vec3i starryVersion => (2, 0, 4);

    /// <summary>
    /// sets up the engine
    /// </summary>
    public static void create(StarrySettings settings)
    {
        Starry.settings = settings;
        string el = $"{settings.gameName} v{settings.gameVersion.x}.{settings.gameVersion.y}.{settings.gameVersion.z}";
        // the size doesn't matter once you make it fullscreen
        Window.create(el, settings.renderSize);
        Window.setFullscreen(settings.fullscreen);

        // fccking kmodules

        settings.startup();

        Sprite sprite = load<Sprite>("restest.png");
        double rot = 0;
        while (!Window.isClosing()) {
            Graphics.clear(color.black);
            Graphics.drawSprite(sprite, (0, 0), (0.5, 0.5), 0, color.white);
            rot += 0.5;
            //Graphics.drawSpriteSuperior(
                //sprite, (0, 0, 50, 50), (50, 50, 50, 100), (0.5, 0.5), 1.5, color.red
            //);
            //Graphics.drawText("Rewolucja przemysłowa i jej konsekwencje okazały się katastrofą dla rodzaju ludzkiego.",
            //    Graphics.defaultFont, (16, 16), color.purple, 16);
            Graphics.endDrawing();
        }

        Window.invokeTheInfamousCloseEventBecauseCeeHashtagIsStupid();

        // fccking kmodules
        Assets.cleanup();
        Window.cleanup();
    }

    /// <summary>
    /// loads the assets and then puts it in a handsome dictionary of stuff so its blazingly fast or smth idfk this is just Assets.load<T> lmao
    /// </summary>
    public static T load<T>(string path) where T: IAsset, new() => Assets.load<T>(path);

    /// <summary>
    /// Console.WriteLine but cooler (it prints more types and has caller information)
    /// </summary>
    public static void log(params object[] x)
    {
        if (!settings.verbose) return;

        StringBuilder str = new();

        // show the class and member lmao
        StackTrace stackTrace = new();
        StackFrame? frame = stackTrace.GetFrame(1);
        var method = frame?.GetMethod();
        var className = method?.DeclaringType?.Name;
        var methodName = method?.Name;
        str.Append($"[{className ?? "unknown"}.{methodName ?? "unknown"}] ");

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
                
                case vec2 wec2: str.Append($"vec2({wec2.x}, {wec2.y})"); break;
                case vec2i wec2i: str.Append($"vec2i({wec2i.x}, {wec2i.y})"); break;
                case vec3 wec3: str.Append($"vec3({wec3.x}, {wec3.y}, {wec3.z})"); break;
                case vec3i wec3i: str.Append($"vec3i({wec3i.x}, {wec3i.y}, {wec3i.z})"); break;
                case color coughlour: str.Append($"rgba({coughlour.r}, {coughlour.g}, {coughlour.b}, {coughlour.a})"); break;
                case null: str.Append("null"); break;
                default: str.Append(JsonConvert.SerializeObject(item, Formatting.Indented)); break;
            }

            if (x.Length > 1) str.Append(", ");
        }
        str.Append('\n');
        Console.Write(str);
    }

    /// <summary>
    /// if true, the game is running in debug mode
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
    /// degree to radian
    /// </summary>
    public static double deg2rad(double deg) => deg * (Math.PI / 180);

    /// <summary>
    /// radian to degree
    /// </summary>
    public static double rad2deg(double rad) => rad * (180 / Math.PI);
}