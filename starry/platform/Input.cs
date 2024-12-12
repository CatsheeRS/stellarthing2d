using System.Collections.Generic;
using System.Linq;
using Silk.NET.GLFW;
namespace starry;

public static class Input {
    // not concurrent because there's only 1 glfw thread,
    // the other threads are just reading this
    static Dictionary<Key, bool> previousKeys = new();
    static Dictionary<Key, bool> currentKeys = new();

    internal static unsafe void keyCallback(WindowHandle* win, Keys keys, int scancode,
    InputAction action, KeyModifiers mods)
    {
        if (action == InputAction.Press) {
            currentKeys[(Key)keys] = true;
            Starry.log("True...");
        }
        else if (action == InputAction.Release) {
            currentKeys[(Key)keys] = false;
            Starry.log("False...");
        }
    }

    internal static void update()
    {
        // .ToDictionary makes a new dictionary
        // TODO: copying a dictionary every frame might be a bit slow
        previousKeys = currentKeys.ToDictionary();
    }

    /// <summary>
    /// if true, the key was just pressed
    /// </summary>
    public static bool isKeyJustPressed(Key key)
    {
        if (!currentKeys.ContainsKey(key) && !previousKeys.ContainsKey(key)) return false;
        return currentKeys[key] && !previousKeys[key];
    }

    /// <summary>
    /// if true, the key is being held down
    /// </summary>
    public static bool isKeyHeld(Key key)
    {
        if (!currentKeys.ContainsKey(key) && !previousKeys.ContainsKey(key)) return false;
        return currentKeys[key] && previousKeys[key];
    }

    /// <summary>
    /// if true, the key just stopped being pressed
    /// </summary>
    public static bool isKeyJustReleased(Key key)
    {
        if (!currentKeys.ContainsKey(key) && !previousKeys.ContainsKey(key)) return false;
        return !currentKeys[key] && previousKeys[key];
    }
}