using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
    public static vec3i starryVersion => (2, 0, 2);

    public static async Task create(StarrySettings settings)
    {
        Starry.settings = settings;
        string el = $"{settings.gameName} v{settings.gameVersion.x}.{settings.gameVersion.y}.{settings.gameVersion.z}";
        // the size doesn't matter once you make it fullscreen
        Window.create(el, settings.renderSize);
        Window.setFullscreen(settings.fullscreen);

        // fccking kmodules

        settings.startup();

        //Sprite sprite = await load<Sprite>("stellarthing.png");
        while (!Window.isClosing()) {
            Graphics.clear(color.purple);
            //Graphics.drawSprite(sprite, (50, 50, 50, 50), 1.5, (1, 0, 0));
            //Graphics.drawText("Rewolucja przemysłowa i jej konsekwencje okazały się katastrofą dla rodzaju ludzkiego.",
            //    Graphics.defaultFont, (16, 16), color.purple, 16);
            Graphics.endDrawing();
        }

        // fccking kmodules
        await Assets.cleanup();
        Window.cleanup();
    }

    /// <summary>
    /// loads the assets and then puts it in a handsome dictionary of stuff so its blazingly fast or smth idfk this is just Assets.load<T> lmao
    /// </summary>
    public static async Task<T> load<T>(string path) where T: IAsset, new() =>
        await Assets.load<T>(path);

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

    public static bool isDebug()
    {
        #if DEBUG
        return true;
        #else
        return false;
        #endif
    }

    /// <summary>
    /// the infamous glm::ortho
    /// </summary>
    public static Matrix4x4 ortho(float left, float right, float bottom, float top,
    float near, float far)
    {
        float rl = 1.0f / (right - left);
        float tb = 1.0f / (top - bottom);
        float fn = 1.0f / (far - near);

        return new Matrix4x4(
            2.0f * rl,  0.0f,       0.0f,      -(right + left) * rl,
            0.0f,       2.0f * tb,  0.0f,      -(top + bottom) * tb,
            0.0f,       0.0f,      -2.0f * fn, -(far + near) * fn,
            0.0f,       0.0f,       0.0f,       1.0f
        );
    }

    /// <summary>
    /// takes a rect and rotation (in degrees) and turns it into a matrix
    /// </summary>
    public static Matrix4x4 transform2matrix(rect2 rect, double rotation)
    {
        Matrix4x4 model = Matrix4x4.Identity;
        model *= Matrix4x4.CreateTranslation((float)rect.x, (float)rect.y, 0);

        model *= Matrix4x4.CreateTranslation(0.5f * (float)rect.w, 0.5f * (float)rect.h, 0);
        model *= Matrix4x4.CreateRotationZ((float)deg2rad(rotation));
        model *= Matrix4x4.CreateScale(-0.5f * (float)rect.w, -0.5f * (float)rect.h, 0);

        model *= Matrix4x4.CreateScale((float)rect.w, (float)rect.h, 1);

        return model;
    }

    public static double deg2rad(double deg) => deg * (Math.PI / 180);
    public static double rad2deg(double rad) => rad * (180 / Math.PI);
}