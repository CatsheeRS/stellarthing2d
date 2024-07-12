using System.Collections.Generic;
using frambos.util;
using Silk.NET.SDL;

namespace frambos.core;

public static class InputManager {
    /// <summary>
    /// the mouse position using the engine's coordinate system (for example the bottom right would always be 1280x720 no matter the resolution)
    /// </summary>
    public static Vector2 mouse_pos { get; internal set; } = Vector2.zero;
    static Dictionary<Key, bool> keys = [];
    static List<Key> just_pressed = [];
    static List<Key> currently_released = [];
    internal static uint lol = 0;

    internal static void handle_new_event(Event e)
    {
        // is it a keyboard thing??????
        if (e.Type == (uint)EventType.Keydown || e.Type == (uint)EventType.Keydown) {
            handle_keyboard(e);
        }
    }

    internal static void handle_keyboard(Event e)
    {
        Key k = scancode_to_frambos_key(e.Key.Keysym.Scancode);

        // pressed
        if (e.Type == (uint)EventType.Keydown) {
            if (keys.ContainsKey(k)) {
                keys[k] = true;
                just_pressed.Add(k);
            }
            else {
                keys.Add(k, true);
                just_pressed.Add(k);
            }
        }
        else if (e.Type == (uint)EventType.Keyup) {
            Frambos.log("g");
            keys[k] = false;
            currently_released.Add(k);
        }
    }

    internal static void handle_keyboard_but_every_frame()
    {
        lol++;
        if (lol == 1) {
            lol = 0;
            just_pressed.Clear();
            currently_released.Clear();
        }
    }

    /// <summary>
    /// if true, the key is currently being held down
    /// </summary>
    public static bool is_key_pressed(Key k)
    {
        if (!keys.TryGetValue(k, out bool value)) return false;
        return value;
    }

    /// <summary>
    /// if true, the key was just pressed
    /// </summary>
    public static bool is_key_just_pressed(Key k) => just_pressed.Contains(k);

    /// <summary>
    /// if true, the key just stopped being pressed
    /// </summary>
    public static bool is_key_released(Key k) => currently_released.Contains(k);

    public static Key scancode_to_frambos_key(Scancode s) => s switch
    {
        Scancode.ScancodeA => Key.a,
        Scancode.ScancodeB => Key.b,
        Scancode.ScancodeC => Key.c,
        Scancode.ScancodeD => Key.d,
        Scancode.ScancodeE => Key.e,
        Scancode.ScancodeF => Key.f,
        Scancode.ScancodeG => Key.g,
        Scancode.ScancodeH => Key.h,
        Scancode.ScancodeI => Key.i,
        Scancode.ScancodeJ => Key.j,
        Scancode.ScancodeK => Key.k,
        Scancode.ScancodeL => Key.l,
        Scancode.ScancodeM => Key.m,
        Scancode.ScancodeN => Key.n,
        Scancode.ScancodeO => Key.o,
        Scancode.ScancodeP => Key.p,
        Scancode.ScancodeQ => Key.q,
        Scancode.ScancodeR => Key.r,
        Scancode.ScancodeS => Key.s,
        Scancode.ScancodeT => Key.t,
        Scancode.ScancodeU => Key.u,
        Scancode.ScancodeV => Key.v,
        Scancode.ScancodeW => Key.w,
        Scancode.ScancodeX => Key.x,
        Scancode.ScancodeY => Key.y,
        Scancode.ScancodeZ => Key.z,
        Scancode.Scancode1 => Key.num1,
        Scancode.Scancode2 => Key.num2,
        Scancode.Scancode3 => Key.num3,
        Scancode.Scancode4 => Key.num4,
        Scancode.Scancode5 => Key.num5,
        Scancode.Scancode6 => Key.num6,
        Scancode.Scancode7 => Key.num7,
        Scancode.Scancode8 => Key.num8,
        Scancode.Scancode9 => Key.num9,
        Scancode.Scancode0 => Key.num0,
        Scancode.ScancodeReturn => Key.enter,
        Scancode.ScancodeEscape => Key.esc,
        Scancode.ScancodeBackspace => Key.backspace,
        Scancode.ScancodeTab => Key.tab,
        Scancode.ScancodeSpace => Key.space,
        Scancode.ScancodeMinus => Key.minus,
        Scancode.ScancodeEquals => Key.equals,
        Scancode.ScancodeLeftbracket => Key.lbracket,
        Scancode.ScancodeRightbracket => Key.rbracket,
        Scancode.ScancodeBackslash => Key.backslash,
        Scancode.ScancodeNonusbackslash => Key.international_backslash,
        Scancode.ScancodeSemicolon => Key.semicolon,
        Scancode.ScancodeApostrophe => Key.apostrophe,
        Scancode.ScancodeGrave => Key.grave,
        Scancode.ScancodeComma => Key.comma,
        Scancode.ScancodePeriod => Key.period,
        Scancode.ScancodeSlash => Key.slash,
        Scancode.ScancodeCapslock => Key.caps_lock,
        Scancode.ScancodeF1 => Key.f1,
        Scancode.ScancodeF2 => Key.f2,
        Scancode.ScancodeF3 => Key.f3,
        Scancode.ScancodeF4 => Key.f4,
        Scancode.ScancodeF5 => Key.f5,
        Scancode.ScancodeF6 => Key.f6,
        Scancode.ScancodeF7 => Key.f7,
        Scancode.ScancodeF8 => Key.f8,
        Scancode.ScancodeF9 => Key.f9,
        Scancode.ScancodeF10 => Key.f10,
        Scancode.ScancodeF11 => Key.f11,
        Scancode.ScancodeF12 => Key.f12,
        Scancode.ScancodePrintscreen => Key.print,
        Scancode.ScancodeScrolllock => Key.scroll_lock,
        Scancode.ScancodePause => Key.pause,
        Scancode.ScancodeInsert => Key.insert,
        Scancode.ScancodeHome => Key.home,
        Scancode.ScancodePageup => Key.page_up,
        Scancode.ScancodePagedown => Key.page_down,
        Scancode.ScancodeEnd => Key.end,
        Scancode.ScancodeLeft => Key.arrow_left,
        Scancode.ScancodeRight => Key.arrow_right,
        Scancode.ScancodeUp => Key.arrow_up,
        Scancode.ScancodeDown => Key.arrow_down,
        Scancode.ScancodeNumlockclear => Key.num_lock,
        Scancode.ScancodeKPDivide => Key.kp_divide,
        Scancode.ScancodeKPMultiply => Key.kp_multiply,
        Scancode.ScancodeKPMinus => Key.kp_minus,
        Scancode.ScancodeKPPlus => Key.kp_plus,
        Scancode.ScancodeKPEnter => Key.kp_enter,
        Scancode.ScancodeKP1 => Key.kp1,
        Scancode.ScancodeKP2 => Key.kp2,
        Scancode.ScancodeKP3 => Key.kp3,
        Scancode.ScancodeKP4 => Key.kp4,
        Scancode.ScancodeKP5 => Key.kp5,
        Scancode.ScancodeKP6 => Key.kp6,
        Scancode.ScancodeKP7 => Key.kp7,
        Scancode.ScancodeKP8 => Key.kp8,
        Scancode.ScancodeKP9 => Key.kp9,
        Scancode.ScancodeKP0 => Key.kp0,
        Scancode.ScancodeLctrl => Key.lctrl,
        Scancode.ScancodeLshift => Key.lshift,
        Scancode.ScancodeLalt => Key.lalt,
        Scancode.ScancodeLgui => Key.lsuper,
        Scancode.ScancodeRctrl => Key.rctrl,
        Scancode.ScancodeRshift => Key.rshift,
        Scancode.ScancodeRalt => Key.ralt,
        Scancode.ScancodeRgui => Key.rsuper,
        _ => Key.unknown
    };

    public static Scancode frambos_key_to_scancode(Key k) => k switch
    {
        Key.a => Scancode.ScancodeA,
        Key.b => Scancode.ScancodeB,
        Key.c => Scancode.ScancodeC,
        Key.d => Scancode.ScancodeD,
        Key.e => Scancode.ScancodeE,
        Key.f => Scancode.ScancodeF,
        Key.g => Scancode.ScancodeG,
        Key.h => Scancode.ScancodeH,
        Key.i => Scancode.ScancodeI,
        Key.j => Scancode.ScancodeJ,
        Key.k => Scancode.ScancodeK,
        Key.l => Scancode.ScancodeL,
        Key.m => Scancode.ScancodeM,
        Key.n => Scancode.ScancodeN,
        Key.o => Scancode.ScancodeO,
        Key.p => Scancode.ScancodeP,
        Key.q => Scancode.ScancodeQ,
        Key.r => Scancode.ScancodeR,
        Key.s => Scancode.ScancodeS,
        Key.t => Scancode.ScancodeT,
        Key.u => Scancode.ScancodeU,
        Key.v => Scancode.ScancodeV,
        Key.w => Scancode.ScancodeW,
        Key.x => Scancode.ScancodeX,
        Key.y => Scancode.ScancodeY,
        Key.z => Scancode.ScancodeZ,
        Key.num1 => Scancode.Scancode1,
        Key.num2 => Scancode.Scancode2,
        Key.num3 => Scancode.Scancode3,
        Key.num4 => Scancode.Scancode4,
        Key.num5 => Scancode.Scancode5,
        Key.num6 => Scancode.Scancode6,
        Key.num7 => Scancode.Scancode7,
        Key.num8 => Scancode.Scancode8,
        Key.num9 => Scancode.Scancode9,
        Key.num0 => Scancode.Scancode0,
        Key.enter => Scancode.ScancodeReturn,
        Key.esc => Scancode.ScancodeEscape,
        Key.backspace => Scancode.ScancodeBackspace,
        Key.tab => Scancode.ScancodeTab,
        Key.space => Scancode.ScancodeSpace,
        Key.minus => Scancode.ScancodeMinus,
        Key.equals => Scancode.ScancodeEquals,
        Key.lbracket => Scancode.ScancodeLeftbracket,
        Key.rbracket => Scancode.ScancodeRightbracket,
        Key.backslash => Scancode.ScancodeBackslash,
        Key.international_backslash => Scancode.ScancodeNonusbackslash,
        Key.semicolon => Scancode.ScancodeSemicolon,
        Key.apostrophe => Scancode.ScancodeApostrophe,
        Key.grave => Scancode.ScancodeGrave,
        Key.comma => Scancode.ScancodeComma,
        Key.period => Scancode.ScancodePeriod,
        Key.slash => Scancode.ScancodeSlash,
        Key.caps_lock => Scancode.ScancodeCapslock,
        Key.f1 => Scancode.ScancodeF1,
        Key.f2 => Scancode.ScancodeF2,
        Key.f3 => Scancode.ScancodeF3,
        Key.f4 => Scancode.ScancodeF4,
        Key.f5 => Scancode.ScancodeF5,
        Key.f6 => Scancode.ScancodeF6,
        Key.f7 => Scancode.ScancodeF7,
        Key.f8 => Scancode.ScancodeF8,
        Key.f9 => Scancode.ScancodeF9,
        Key.f10 => Scancode.ScancodeF10,
        Key.f11 => Scancode.ScancodeF11,
        Key.f12 => Scancode.ScancodeF12,
        Key.print => Scancode.ScancodePrintscreen,
        Key.scroll_lock => Scancode.ScancodeScrolllock,
        Key.pause => Scancode.ScancodePause,
        Key.insert => Scancode.ScancodeInsert,
        Key.home => Scancode.ScancodeHome,
        Key.page_up => Scancode.ScancodePageup,
        Key.page_down => Scancode.ScancodePagedown,
        Key.end => Scancode.ScancodeEnd,
        Key.arrow_left => Scancode.ScancodeLeft,
        Key.arrow_right => Scancode.ScancodeRight,
        Key.arrow_up => Scancode.ScancodeUp,
        Key.arrow_down => Scancode.ScancodeDown,
        Key.num_lock => Scancode.ScancodeNumlockclear,
        Key.kp_divide => Scancode.ScancodeKPDivide,
        Key.kp_multiply => Scancode.ScancodeKPMultiply,
        Key.kp_minus => Scancode.ScancodeKPMinus,
        Key.kp_plus => Scancode.ScancodeKPPlus,
        Key.kp_enter => Scancode.ScancodeKPEnter,
        Key.kp1 => Scancode.ScancodeKP1,
        Key.kp2 => Scancode.ScancodeKP2,
        Key.kp3 => Scancode.ScancodeKP3,
        Key.kp4 => Scancode.ScancodeKP4,
        Key.kp5 => Scancode.ScancodeKP5,
        Key.kp6 => Scancode.ScancodeKP6,
        Key.kp7 => Scancode.ScancodeKP7,
        Key.kp8 => Scancode.ScancodeKP8,
        Key.kp9 => Scancode.ScancodeKP9,
        Key.kp0 => Scancode.ScancodeKP0,
        Key.lctrl => Scancode.ScancodeLctrl,
        Key.lshift => Scancode.ScancodeLshift,
        Key.lalt => Scancode.ScancodeLalt,
        Key.lsuper => Scancode.ScancodeLgui,
        Key.rctrl => Scancode.ScancodeRctrl,
        Key.rshift => Scancode.ScancodeRshift,
        Key.ralt => Scancode.ScancodeRalt,
        Key.rsuper => Scancode.ScancodeRgui,
        _ => Scancode.ScancodeUnknown
    };
}

public enum Key
{
    unknown,

    // letters
    a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,

    // numbers above letters
    num1, num2, num3, num4, num5, num6, num7, num8, num9, num0,

    // uh
    // enter is also known as return (which is a keyword)
    enter, esc, backspace, tab, space,

    // punctuation and stuff
    minus, equals, lbracket, rbracket, backslash, semicolon, apostrophe, grave, comma, period, slash,

    caps_lock,

    // the Fs
    f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12,

    // pause and scroll lock isn't on all keyboards, not sure about home, end, and the pages
    print, scroll_lock, pause, insert, num_lock, home, page_up, page_down, delete, end,

    // arrow keys
    arrow_left, arrow_right, arrow_up, arrow_down,

    // numpad, not on all keyboards
    kp_divide, kp_multiply, kp_minus, kp_plus, kp_enter, kp_period, kp1, kp2, kp3, kp4, kp5, kp6, kp7, kp8, kp9, kp0,

    // additional key not on us keyboards, between left shift and z/y (qwertz exists)
    international_backslash,

    // ralt is sometimes alt gr, super is the windows key
    lctrl, lshift, lalt, lsuper, rctrl, rshift, ralt, rsuper,
}

enum KeyState
{
    not_pressed,
    just_pressed,
    pressed,
    released
}