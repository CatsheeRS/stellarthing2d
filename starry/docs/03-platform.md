# Platform

This is a layer of abstraction over raylib so i can decide to not use raylib if i want to

Here's some code examples

```cs
// making a window
Window.create("Starry 2©®©™®©®™©®©™©™®", (1280, 720));
await Graphics.create();

while (!Window.isClosing()) {
    Graphics.clear(color.white);
    Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
    Graphics.endDrawing();
}

await Graphics.cleanup();
Window.cleanup();
``