using Silk.NET.GLFW;
using static starry.Starry;

namespace starry;

public static class Application {
    public static Vector2i screenSize { get; private set; } = vec2i();
    static Glfw? glfw;

    public unsafe static void create()
    {
        // init stuff
        glfw = Glfw.GetApi();
        if (!glfw.Init()) {
            log("Fatal error: couldn't start GLFW");
            return;
        }

        // get screen width and height and stuff
        Monitor* monitor = glfw.GetPrimaryMonitor();
        if (monitor == null) {
            log("Fatal error: couldn't get primary monitor");
            glfw.Terminate();
            return;
        }

        VideoMode* mode = glfw.GetVideoMode(monitor);
        if (mode == null) {
            log("Fatal error: couldn't get video mode");
            glfw.Terminate();
            return;
        }

        // hints and stuff
        glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
        glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
        glfw.WindowHint(WindowHintInt.RefreshRate, 60);
        
        // make window!!!!!!1
        WindowHandle* window = glfw.CreateWindow(mode->Width, mode->Height, settings.gameName, monitor, null);
        if (window == null) {
            log("Fatal error: couldn't create window");
            glfw.Terminate();
            return;
        }
        glfw.MakeContextCurrent(window);

        // main loop
        while (!glfw.WindowShouldClose(window)) {
            glfw.PollEvents();

            // rendering goes here

            glfw.SwapBuffers(window);
        }

        // exit
        glfw.DestroyWindow(window);
        glfw.Terminate();
        return;
    }
}