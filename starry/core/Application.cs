using System;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages the lifecycle of the game
/// </summary>
public static class Application {
    /// <summary>
    /// the screen size
    /// </summary>
    public static Vector2i screenSize { get; private set; } = vec2i();
    /// <summary>
    /// current delta time
    /// </summary>
    public static double delta { get; set; } = 0;
    static double prevtime;
    static Glfw? glfw;

    public static event EventHandler? onClose;

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
        // it's not fullscreen in debug mode since it's nicer that way
        // TODO: add fullscreen and resolution options :D
        #if DEBUG
        WindowHandle* window = glfw.CreateWindow(mode->Width, mode->Height, settings.gameName, null, null);
        #else
        WindowHandle* window = glfw.CreateWindow(mode->Width, mode->Height, settings.gameName, monitor, null);
        #endif
        if (window == null) {
            log("Fatal error: couldn't create window");
            glfw.Terminate();
            return;
        }
        glfw.MakeContextCurrent(window);

        // setup opengl
        GL gl = GL.GetApi(glfw.GetProcAddress);
        GLRenderer.create(gl, glfw, window);
        
        // callbacks
        glfw.SetWindowCloseCallback(window, (window) => {
            onClose?.Invoke(typeof(Application), EventArgs.Empty);
            // TODO: allow not always closing windows and stuff
            glfw.SetWindowShouldClose(window, true);
        });
        
        glfw.SetKeyCallback(window, (window, key, scancode, action, mods) => {
            World.sendKeyCallbacks(key, action);
        });

        glfw.SetCursorPosCallback(window, (window, xpos, ypos) => {
            Input.mousePosition = vec2(xpos, ypos);
        });

        glfw.SetMouseButtonCallback(window, (window, button, action, mods) => {
            MouseButton? elmierda = button switch {
                Silk.NET.GLFW.MouseButton.Left => MouseButton.left,
                Silk.NET.GLFW.MouseButton.Right => MouseButton.right,
                Silk.NET.GLFW.MouseButton.Middle => MouseButton.middle,
                _ => null
            };

            MouseButtonState? province = action switch {
                InputAction.Press => MouseButtonState.justPressed,
                InputAction.Release => MouseButtonState.released,
                _ => null,
            };

            // fuck off
            if (elmierda == null || province == null) return;
            World.sendMouseButtonCallbacks((MouseButton)elmierda, (MouseButtonState)province);
        });

        glfw.SetErrorCallback((error, description) => {
            log("OPENGL ERROR: ", error.ToString(), description);
        });

        prevtime = glfw.GetTime();
        World.create(glfw);
        settings.startup();

        // main loop
        while (!glfw.WindowShouldClose(window)) {
            glfw.PollEvents();

            // get delta time :D
            double delta = glfw.GetTime() - prevtime;
            prevtime = glfw.GetTime();
            
            // i'm used to pressing f5 to start and f8 to stop in godot
            #if DEBUG
            if (glfw.GetKey(window, Keys.F8) == (int)InputAction.Press) {
                glfw.SetWindowShouldClose(window, true);
            }
            #endif

            // rendering goes here
            Input.update(delta);
            World.updateEntities();

            glfw.SwapBuffers(window);
        }

        // exit
        glfw.DestroyWindow(window);
        glfw.Terminate();
        return;
    }
}