using System.Collections.Generic;
using System.Threading.Tasks;
using Raylib_cs;
namespace starry;

public class Starry {
    /// <summary>
    /// starry settings
    /// </summary>
    public static StarrySettings settings { get; set; }
    /// <summary>
    /// the engine version (semantic versioning)
    /// </summary>
    public static vec3i starryVersion => (2, 0, 0);

    public static async Task create(StarrySettings settings)
    {
        Starry.settings = settings;
        Raylib.InitWindow(800, 480,
            // quite the mouthful
            $"{settings.gameName} v{settings.gameVersion.x}.{settings.gameVersion.y}.{settings.gameVersion.z}");

        // modules :D
        await TestModule.create();

        while (!Raylib.WindowShouldClose()) {
            // modules :D
            await TestModule.update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
            Raylib.EndDrawing();
        }

        // modules :D
        await TestModule.cleanup();
        Raylib.CloseWindow();
    }
}