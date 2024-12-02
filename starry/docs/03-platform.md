# Platform

This is a layer of abstraction over raylib so i can decide to not use raylib if i want to

Here's some code examples

```cs
// making a window
Window.create("Starry 2©®©™®©®™©®©™©™®", (1280, 720));
await Graphics.create();

while (!Window.isClosing()) {
    Graphics.clear(color.white);
    Graphics.drawText("Hi mom", Graphics.defaultFont, (16, 16), color.black, 16);
    Graphics.endDrawing();
}

await Graphics.cleanup();
Window.cleanup();
``