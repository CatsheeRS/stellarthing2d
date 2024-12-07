# Graphics

Starry has a very handsome renderer.

It uses [Skia](https://skia.org) (through [SkiaSharp](https://github.com/mono/SkiaSharp)) for rendering and GLFW (through [Silk.NET](https://github.com/dotnet/Silk.NET)) for windowing

OpenGL is single-threaded so the Starry renderer also has a thread running an event loop waiting for rendering actions, which is very convenient since async/await

To access the renderer simply use the `Graphics` functions