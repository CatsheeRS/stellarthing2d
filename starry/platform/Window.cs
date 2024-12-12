using System;
using System.Threading.Tasks;
using Silk.NET.GLFW;
using Silk.NET.Windowing;

namespace starry;

/// <summary>
/// the game window. static since i don't think games need several of those
/// </summary>
public static unsafe class Window {
    /// <summary>
    /// called when the window is resized
    /// </summary>
    public static event ResizeEvent? onResize;
    /// <summary>
    /// called right before the engine starts cleaning up
    /// </summary>
    public static event EventHandler? onClose;
    /// <summary>
    /// delta time in seconds
    /// </summary>
    public static double deltaTime { get; private set; } = 0;
    /// <summary>
    /// time since the engine started, in seconds
    /// </summary>
    public static double elapsedTime { get; private set; } = 0;
    /// <summary>
    /// the fps the game is running on
    /// </summary>
    public static double fps { get; private set; } = 0;

    internal static IWindow? window;

    /// <summary>
    /// creates the window :D
    /// </summary>
    public static unsafe void create(string title, vec2i size, Action create, Action<double> update,
    Action cleanup)
    {
        Graphics.actions.Enqueue(() => {
            window = Silk.NET.Windowing.Window.Create(new WindowOptions() {
                // i dont even know anymore
                Size = new Silk.NET.Maths.Vector2D<int>((int)Starry.settings.renderSize.x, (int)Starry.settings.renderSize.y),
                Title = title,
                FramesPerSecond = Starry.settings.fps,
                UpdatesPerSecond = Starry.settings.fps,
                WindowState = Starry.settings.fullscreen ? WindowState.Fullscreen : WindowState.Normal,
                WindowBorder = WindowBorder.Resizable,
            });

            // there's a lot of callbacks
            window.Load += () => {
                Graphics.create();

                create();
            };

            window.Render += (delta) => {
                deltaTime = delta;
                elapsedTime += delta;
                fps = 1 / deltaTime;

                update(delta);
            };

            window.Closing += () => {
                onClose?.Invoke(null, EventArgs.Empty);

                Graphics.cleanup();

                cleanup();
            };

            window.FramebufferResize += why => {
                onResize?.Invoke((why.X, why.Y));
            };

            Starry.log("Created window");

            // why
            window.Run();
            window.Dispose();
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// if true, the window is gonna be fullscreen
    /// </summary>
    public static void setFullscreen(bool fullscreen)
    {
        Graphics.actions.Enqueue(() => {
            if (window == null) return;
            window.WindowState = fullscreen ? WindowState.Fullscreen : WindowState.Normal;
            if (fullscreen) Starry.log("Window is now fullscreen");
            else Starry.log("Windows is now windowed");
        });
        Graphics.actionLoopEvent.Set();
    }

    /// <summary>
    /// if true, the window is currently fullscreen
    /// </summary>
    public static bool isFullscreen()
    {
        if (window == null) return false;
        return window.WindowState == WindowState.Fullscreen;
    }

    /// <summary>
    /// the size of the framebuffer
    /// </summary>
    public static Task<vec2i> getSize()
    {
        TaskCompletionSource<vec2i> tcs = new();
            Graphics.actions.Enqueue(() => {
            if (window == null) {
                tcs.SetResult((0, 0));
                return;
            }

            var aaaaaajjjj = window.FramebufferSize;
            tcs.SetResult((aaaaaajjjj.X, aaaaaajjjj.Y));
        });
        Graphics.actionLoopEvent.Set();
        return tcs.Task;
    }

    public delegate void ResizeEvent(vec2i newSize);
}