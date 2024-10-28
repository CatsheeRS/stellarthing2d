using System;
// help
using static starry.Starry;
using static SDL2.SDL;
using static SDL2.SDL_image;
using SDL2;
namespace starry;

/// <summary>
/// comically large class for platform abstractions. this currently uses SDL2 but i may change it.
/// </summary>
public static partial class Platform
{
    internal static WindowSettings platsettings;
    internal static nint window;
    internal static ulong startTicks = 0;
    internal static double fps = 0;
    internal static nint sdlRender;
    internal static nint screenSurface;

    /// <summary>
    /// creates the window and stuff
    /// </summary>
    /// <param name="settings"></param>
    public static void createWindow(WindowSettings settings)
    {
        // TODO: don't init everything, don't init if it's just a server
        if (SDL_Init(SDL_INIT_EVERYTHING) < 0) {
            log("FATAL ERROR: SDL couldn't initialize.");
            return;
        }

        // make flags since it's kinda fucky
        SDL_WindowFlags flags = 0;
        flags |= settings.type switch {
            WindowType.windowed => 0,
            WindowType.fullscreen => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
            WindowType.borderless => SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            WindowType.fullscreenBorderless => SDL_WindowFlags.SDL_WINDOW_FULLSCREEN |
                                               SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            _ => throw new Exception("you shouldn't do that with the WindowType enum"),
        };

        window = SDL_CreateWindow(settings.title, 100, 100, settings.size.x, settings.size.y, flags);
        if (window == 0) {
            log("FATAL ERROR: Couldn't create window");
            return;
        }

        sdlRender = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        if (sdlRender == 0) {
            log("FATAL ERROR: Couldn't create renderer");
            return;
        }

        screenSurface = SDL_GetWindowSurface(window);

        platsettings = settings;
        createRendererSubsystemThing();
    }

    /// <summary>
    /// returns the time that has elapsed since the window was created, in milliseconds
    /// </summary>
    public static ulong getTime()
    {
        return SDL_GetTicks64();
    }

    /// <summary>
    /// it handles events :D
    /// </summary>
    internal static void handleEvents(SDL_Event e)
    {
        // hell
        if (e.type == SDL_EventType.SDL_KEYDOWN) {
            Key k = sdlToStarryKey(e.key.keysym.scancode);
            if ((Input.keystates.TryGetValue(k, out byte v) ? v : Input.inactive) == Input.inactive) {
                if (!Input.keystates.TryAdd(k, Input.justPressed)) Input.keystates[k] = Input.justPressed;
            }
            else if ((Input.keystates.TryGetValue(k, out byte w) ? w : Input.inactive) == Input.justPressed) {
                if (!Input.keystates.TryAdd(k, Input.pressed)) Input.keystates[k] = Input.pressed;
            }
        }
        else if (e.type == SDL_EventType.SDL_KEYUP) {
            Key k = sdlToStarryKey(e.key.keysym.scancode);
            if (!Input.keystates.TryAdd(k, Input.justReleased)) Input.keystates[k] = Input.justReleased;
        }

        else if (e.type == SDL_EventType.SDL_MOUSEBUTTONDOWN) {
            if (Input.mousefuckingstate[e.button.button - 1] == Input.inactive) {
                Input.mousefuckingstate[e.button.button - 1] = Input.justPressed;
            }
            else if (Input.mousefuckingstate[e.button.button - 1] == Input.justPressed) {
                Input.mousefuckingstate[e.button.button - 1] = Input.pressed;
            }
        }
        else if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP) {
            Input.mousefuckingstate[e.button.button - 1] = Input.justReleased;
        }
    }

    internal static Key sdlToStarryKey(SDL_Scancode scancode)
    {
        return scancode switch {
            SDL_Scancode.SDL_SCANCODE_A => Key.a,
            SDL_Scancode.SDL_SCANCODE_B => Key.b,
            SDL_Scancode.SDL_SCANCODE_C => Key.c,
            SDL_Scancode.SDL_SCANCODE_D => Key.d,
            SDL_Scancode.SDL_SCANCODE_E => Key.e,
            SDL_Scancode.SDL_SCANCODE_F => Key.f,
            SDL_Scancode.SDL_SCANCODE_G => Key.g,
            SDL_Scancode.SDL_SCANCODE_H => Key.h,
            SDL_Scancode.SDL_SCANCODE_I => Key.i,
            SDL_Scancode.SDL_SCANCODE_J => Key.j,
            SDL_Scancode.SDL_SCANCODE_K => Key.k,
            SDL_Scancode.SDL_SCANCODE_L => Key.l,
            SDL_Scancode.SDL_SCANCODE_M => Key.m,
            SDL_Scancode.SDL_SCANCODE_N => Key.n,
            SDL_Scancode.SDL_SCANCODE_O => Key.o,
            SDL_Scancode.SDL_SCANCODE_P => Key.p,
            SDL_Scancode.SDL_SCANCODE_Q => Key.q,
            SDL_Scancode.SDL_SCANCODE_R => Key.r,
            SDL_Scancode.SDL_SCANCODE_S => Key.s,
            SDL_Scancode.SDL_SCANCODE_T => Key.t,
            SDL_Scancode.SDL_SCANCODE_U => Key.u,
            SDL_Scancode.SDL_SCANCODE_V => Key.v,
            SDL_Scancode.SDL_SCANCODE_W => Key.w,
            SDL_Scancode.SDL_SCANCODE_X => Key.x,
            SDL_Scancode.SDL_SCANCODE_Y => Key.y,
            SDL_Scancode.SDL_SCANCODE_Z => Key.z,
            SDL_Scancode.SDL_SCANCODE_1 => Key.key1,
            SDL_Scancode.SDL_SCANCODE_2 => Key.key2,
            SDL_Scancode.SDL_SCANCODE_3 => Key.key3,
            SDL_Scancode.SDL_SCANCODE_4 => Key.key4,
            SDL_Scancode.SDL_SCANCODE_5 => Key.key5,
            SDL_Scancode.SDL_SCANCODE_6 => Key.key6,
            SDL_Scancode.SDL_SCANCODE_7 => Key.key7,
            SDL_Scancode.SDL_SCANCODE_8 => Key.key8,
            SDL_Scancode.SDL_SCANCODE_9 => Key.key9,
            SDL_Scancode.SDL_SCANCODE_0 => Key.key0,
            SDL_Scancode.SDL_SCANCODE_RETURN => Key.enter,
            SDL_Scancode.SDL_SCANCODE_ESCAPE => Key.escape,
            SDL_Scancode.SDL_SCANCODE_BACKSPACE => Key.backspace,
            SDL_Scancode.SDL_SCANCODE_TAB => Key.tab,
            SDL_Scancode.SDL_SCANCODE_SPACE => Key.space,
            SDL_Scancode.SDL_SCANCODE_MINUS => Key.minus,
            SDL_Scancode.SDL_SCANCODE_EQUALS => Key.equal,
            SDL_Scancode.SDL_SCANCODE_LEFTBRACKET => Key.leftBracket,
            SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET => Key.rightBracket,
            SDL_Scancode.SDL_SCANCODE_BACKSLASH => Key.backslash,
            SDL_Scancode.SDL_SCANCODE_SEMICOLON => Key.semicolon,
            SDL_Scancode.SDL_SCANCODE_APOSTROPHE => Key.apostrophe,
            SDL_Scancode.SDL_SCANCODE_GRAVE => Key.grave,
            SDL_Scancode.SDL_SCANCODE_COMMA => Key.comma,
            SDL_Scancode.SDL_SCANCODE_PERIOD => Key.period,
            SDL_Scancode.SDL_SCANCODE_SLASH => Key.slash,
            SDL_Scancode.SDL_SCANCODE_CAPSLOCK => Key.capsLock,
            SDL_Scancode.SDL_SCANCODE_F1 => Key.f1,
            SDL_Scancode.SDL_SCANCODE_F2 => Key.f2,
            SDL_Scancode.SDL_SCANCODE_F3 => Key.f3,
            SDL_Scancode.SDL_SCANCODE_F4 => Key.f4,
            SDL_Scancode.SDL_SCANCODE_F5 => Key.f5,
            SDL_Scancode.SDL_SCANCODE_F6 => Key.f6,
            SDL_Scancode.SDL_SCANCODE_F7 => Key.f7,
            SDL_Scancode.SDL_SCANCODE_F8 => Key.f8,
            SDL_Scancode.SDL_SCANCODE_F9 => Key.f9,
            SDL_Scancode.SDL_SCANCODE_F10 => Key.f10,
            SDL_Scancode.SDL_SCANCODE_F11 => Key.f11,
            SDL_Scancode.SDL_SCANCODE_F12 => Key.f12,
            SDL_Scancode.SDL_SCANCODE_PRINTSCREEN => Key.print,
            SDL_Scancode.SDL_SCANCODE_SCROLLLOCK => Key.scrollLock,
            SDL_Scancode.SDL_SCANCODE_PAUSE => Key.pause,
            SDL_Scancode.SDL_SCANCODE_INSERT => Key.insert,
            SDL_Scancode.SDL_SCANCODE_HOME => Key.home,
            SDL_Scancode.SDL_SCANCODE_PAGEUP => Key.pageUp,
            SDL_Scancode.SDL_SCANCODE_DELETE => Key.delete,
            SDL_Scancode.SDL_SCANCODE_END => Key.end,
            SDL_Scancode.SDL_SCANCODE_PAGEDOWN => Key.pageDown,
            SDL_Scancode.SDL_SCANCODE_RIGHT => Key.arrowRight,
            SDL_Scancode.SDL_SCANCODE_LEFT => Key.arrowLeft,
            SDL_Scancode.SDL_SCANCODE_DOWN => Key.arrowDown,
            SDL_Scancode.SDL_SCANCODE_UP => Key.arrowUp,
            SDL_Scancode.SDL_SCANCODE_KP_DIVIDE => Key.kpDivide,
            SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY => Key.kpMultiply,
            SDL_Scancode.SDL_SCANCODE_KP_MINUS => Key.kpSubtract,
            SDL_Scancode.SDL_SCANCODE_KP_PLUS => Key.kpAdd,
            SDL_Scancode.SDL_SCANCODE_KP_ENTER => Key.kpEnter,
            SDL_Scancode.SDL_SCANCODE_KP_1 => Key.kp1,
            SDL_Scancode.SDL_SCANCODE_KP_2 => Key.kp2,
            SDL_Scancode.SDL_SCANCODE_KP_3 => Key.kp3,
            SDL_Scancode.SDL_SCANCODE_KP_4 => Key.kp4,
            SDL_Scancode.SDL_SCANCODE_KP_5 => Key.kp5,
            SDL_Scancode.SDL_SCANCODE_KP_6 => Key.kp6,
            SDL_Scancode.SDL_SCANCODE_KP_7 => Key.kp7,
            SDL_Scancode.SDL_SCANCODE_KP_8 => Key.kp8,
            SDL_Scancode.SDL_SCANCODE_KP_9 => Key.kp9,
            SDL_Scancode.SDL_SCANCODE_KP_0 => Key.kp0,
            SDL_Scancode.SDL_SCANCODE_KP_PERIOD => Key.kpDecimal,
            SDL_Scancode.SDL_SCANCODE_KP_EQUALS => Key.kpEqual,
            SDL_Scancode.SDL_SCANCODE_MENU => Key.menu,
            SDL_Scancode.SDL_SCANCODE_LCTRL => Key.leftControl,
            SDL_Scancode.SDL_SCANCODE_LSHIFT => Key.leftShift,
            SDL_Scancode.SDL_SCANCODE_LALT => Key.leftAlt,
            SDL_Scancode.SDL_SCANCODE_RCTRL => Key.rightControl,
            SDL_Scancode.SDL_SCANCODE_RSHIFT => Key.rightShift,
            SDL_Scancode.SDL_SCANCODE_RALT => Key.rightAlt,
            _ => Key.unknown
        };
    }

    // if true, the window requested to be close
    public static bool shouldClose()
    {
        while (SDL_PollEvent(out SDL_Event e) != 0) {
            if (e.type == SDL_EventType.SDL_QUIT) return true;
            else handleEvents(e);
        }
        return false;
    }

    /// <summary>
    /// you should run this at the start of your main loop for things to work. this also clears the screen
    /// </summary>
    public static void startUpdate()
    {
        startTicks = SDL_GetTicks64();
        
        SDL_GetMouseState(out int mx, out int my);
        // fucking virtual mouse stuff
        vec2 virt = vec2();
        virt.x = (mx - ((getScreenSize().x - (platsettings.renderSize.x * renderScale)) * 0.5)) / renderScale;
        virt.y = (mx - ((getScreenSize().y - (platsettings.renderSize.y * renderScale)) * 0.5)) / renderScale;
        virt = vec2(Math.Clamp(virt.x, 0, platsettings.renderSize.x), Math.Clamp(virt.y, 0, platsettings.renderSize.y));
        Input.mousePosition = vec2i((int)virt.x, (int)virt.y);
        
        SDL_RenderClear(sdlRender);
    }

    /// <summary>
    /// you should run this at the end of your main loop for things to work
    /// </summary>
    public static void endUpdate()
    {
        ulong endTicks = SDL_GetTicks64();
        fps = 1 / ((endTicks - startTicks) / 1000f);

        // unrelease stuff after the game ran stuff
        foreach (var labiovelarfricative in Input.keystates) {
            if (Input.keystates[labiovelarfricative.Key] == Input.justReleased) {
                Input.keystates[labiovelarfricative.Key] = Input.inactive;
            }
        }

        int i = 0;
        foreach (var fromwikipediathefreeencyclopedia in Input.mousefuckingstate) {
            if (fromwikipediathefreeencyclopedia == Input.justReleased) {
                Input.mousefuckingstate[i] = Input.inactive;
            }
            i++;
        }

        SDL_RenderPresent(sdlRender);
    }

    /// <summary>
    /// the current fps of the game
    /// </summary>
    public static double getFps()
    {
        return fps;
    }

    /// <summary>
    /// cleans up the internal stuff
    /// </summary>
    public static void cleanup()
    {
        SDL_DestroyWindow(window);
        SDL_DestroyRenderer(sdlRender);
        SDL_Quit();
    }
}
