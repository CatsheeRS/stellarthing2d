using System;
using Silk.NET.GLFW;
using Veldrid.StartupUtilities;
using Veldrid;
using static starry.Starry;
using Veldrid.Sdl2;

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

    public static event EventHandler? onClose;

    public unsafe static void create()
    {
        WindowCreateInfo winfo = new() {
            X = 100,
            Y = 100 ,
            WindowWidth = 960,
            WindowHeight = 540,
            WindowTitle = settings.gameName,
            WindowInitialState = WindowState.FullScreen,
        };
        Sdl2Window window = VeldridStartup.CreateWindow(ref winfo);

        VeldridStartup.CreateGraphicsDevice(window, new GraphicsDeviceOptions {

        });
        
        // callbacks
        window.Closed += () => onClose?.Invoke(typeof(Application), EventArgs.Empty);
        window.KeyDown += (keyevent) => World.sendKeyCallbacks(keyevent.Key, KeypressState.justPressed);
        
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
            log("GLFW ERROR: ", error.ToString(), description);
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