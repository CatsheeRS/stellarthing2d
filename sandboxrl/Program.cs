using System;
using System.Numerics;
using System.Linq;
using Raylib_cs;

Raylib.InitWindow(640, 480, "raylib sandbox");
Raylib.SetTargetFPS(60);

string codepoints = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHI\nJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmn\nopqrstuvwxyz{|}~¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓ\nÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷\nøùúûüýþÿ";

Font font = Raylib.LoadFontEx("assets/sandbox/RedHatText-Bold.ttf", 16, Array.ConvertAll(codepoints.ToCharArray(), c => (int)char.GetNumericValue(c)), 250);

while (!Raylib.WindowShouldClose()) {
    Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        //Raylib.DrawText("Thé qúíćk bŕóẃń fóx júmpéd óvéŕ thé ĺáźý dóǵ.", 100, 100, 20, Color.White);
        Raylib.DrawTextEx(font, codepoints, new Vector2(100, 100), 16, 0, Color.White);
        // DrawTextEx(fontTtf, msg, (Vector2){ 20.0f, 100.0f }, (float)fontTtf.baseSize, 2, LIME);
    Raylib.EndDrawing();
} 

Raylib.CloseWindow();