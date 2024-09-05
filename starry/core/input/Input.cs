using System;
using System.Collections.Generic;
using Silk.NET.GLFW;
using static starry.Starry;

namespace starry;

public static class Input {
    // you should minimize this
    internal static Dictionary<Key, KeyInfo> keyinfo { get; set; } = new() {
        { Key.space, new() },
        { Key.apostrophe, new() },
        { Key.comma, new() },
        { Key.minus, new() },
        { Key.period, new() },
        { Key.slash, new() },
        { Key.key0, new() },
        { Key.key1, new() },
        { Key.key2, new() },
        { Key.key3, new() },
        { Key.key4, new() },
        { Key.key5, new() },
        { Key.key6, new() },
        { Key.key7, new() },
        { Key.key8, new() },
        { Key.key9, new() },
        { Key.semicolon, new() },
        { Key.equal, new() },
        { Key.a, new() },
        { Key.b, new() },
        { Key.c, new() },
        { Key.d, new() },
        { Key.e, new() },
        { Key.f, new() },
        { Key.g, new() },
        { Key.h, new() },
        { Key.i, new() },
        { Key.j, new() },
        { Key.k, new() },
        { Key.l, new() },
        { Key.m, new() },
        { Key.n, new() },
        { Key.o, new() },
        { Key.p, new() },
        { Key.q, new() },
        { Key.r, new() },
        { Key.s, new() },
        { Key.t, new() },
        { Key.u, new() },
        { Key.v, new() },
        { Key.w, new() },
        { Key.x, new() },
        { Key.y, new() },
        { Key.z, new() },
        { Key.leftBracket, new() },
        { Key.backslash, new() },
        { Key.rightBracket, new() },
        { Key.grave, new() },
        { Key.international1, new() },
        { Key.international2, new() },
        { Key.escape, new() },
        { Key.enter, new() },
        { Key.tab, new() },
        { Key.backspace, new() },
        { Key.insert, new() },
        { Key.delete, new() },
        { Key.arrowRight, new() },
        { Key.arrowLeft, new() },
        { Key.arrowDown, new() },
        { Key.arrowUp, new() },
        { Key.pageUp, new() },
        { Key.pageDown, new() },
        { Key.home, new() },
        { Key.end, new() },
        { Key.capsLock, new() },
        { Key.scrollLock, new() },
        { Key.numLock, new() },
        { Key.print, new() },
        { Key.pause, new() },
        { Key.f1, new() },
        { Key.f2, new() },
        { Key.f3, new() },
        { Key.f4, new() },
        { Key.f5, new() },
        { Key.f6, new() },
        { Key.f7, new() },
        { Key.f8, new() },
        { Key.f9, new() },
        { Key.f10, new() },
        { Key.f11, new() },
        { Key.f12, new() },
        { Key.f13, new() },
        { Key.f14, new() },
        { Key.f15, new() },
        { Key.f16, new() },
        { Key.f17, new() },
        { Key.f18, new() },
        { Key.f19, new() },
        { Key.f20, new() },
        { Key.f21, new() },
        { Key.f22, new() },
        { Key.f23, new() },
        { Key.f24, new() },
        { Key.f25, new() },
        { Key.kp0, new() },
        { Key.kp1, new() },
        { Key.kp2, new() },
        { Key.kp3, new() },
        { Key.kp4, new() },
        { Key.kp5, new() },
        { Key.kp6, new() },
        { Key.kp7, new() },
        { Key.kp8, new() },
        { Key.kp9, new() },
        { Key.kpDecimal, new() },
        { Key.kpDivide, new() },
        { Key.kpMultiply, new() },
        { Key.kpSubtract, new() },
        { Key.kpAdd, new() },
        { Key.kpEnter, new() },
        { Key.kpEqual, new() },
        { Key.leftShift, new() },
        { Key.leftControl, new() },
        { Key.leftAlt, new() },
        { Key.leftSuper, new() },
        { Key.rightShift, new() },
        { Key.rightControl, new() },
        { Key.rightAlt, new() },
        { Key.rightSuper, new() },
        { Key.menu, new() },
    };
    internal static List<Key> pressed { get; set; } = [];

    // called every frame to do some check stuff
    internal static void update(double delta)
    {
        foreach (Key key in pressed) {
            KeyInfo kinf = keyinfo[key];
            // TODO: if you change the frame rate then you have to change how many frames are in a second
            // a key should only be in the just pressed or release for 1 frame
            if (kinf.secondsPressed > 0.016 && kinf.state == KeypressType.justPressed) {
                kinf.state = KeypressType.pressed;
            }

            if (kinf.secondsPressed > 0.016 && kinf.state == KeypressType.released) {
                kinf.state = KeypressType.inactive;
            }

            kinf.secondsPressed += delta;
        }
    }

    internal static void setKeyState(Key key, InputAction state)
    {
        KeypressType systate = state switch {
            InputAction.Press => KeypressType.justPressed,
            InputAction.Release => KeypressType.released,
            InputAction.Repeat => KeypressType.pressed,
            _ => throw new Exception(), // c# stop complaining
        };
        pressed.Add(key);
        var hola = keyinfo[key];
        hola.state = systate;
    }
}

public class KeyInfo
{
    public KeyInfo() {}
    public double secondsPressed { get; set; } = 0;
    public KeypressType state { get; set; } = KeypressType.inactive;
}