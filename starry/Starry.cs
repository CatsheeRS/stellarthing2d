using System.Collections.Generic;
using System.Threading.Tasks;
using Raylib_cs;
namespace starry;

public class Starry {
    public static StarrySettings settings { get; set; }

    public static async Task create(StarrySettings settings)
    {
        Starry.settings = settings;
        Raylib.InitWindow(800, 480, "Hello World");

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