/* checks if raylib works */
#pragma once
#include <raylib.h>

namespace sandbox {

void raylib()
{
    const int scrw = 800;
    const int scrh = 450;

    InitWindow(scrw, scrh, "raylib test");
    SetTargetFPS(60);

    while (!WindowShouldClose()) {
        BeginDrawing();
            ClearBackground(RAYWHITE);
            DrawText("Congrats! You created your first window!", 190, 200, 20, LIGHTGRAY);
        EndDrawing();
    }

    CloseWindow();
}

}