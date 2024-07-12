using System;
using frambos.core;

namespace frambos;

/// <summary>
/// many general-purpose static functions and properties
/// </summary>
public static class Frambos
{
    public static bool verbose_mode { get; internal set; } = false;

    /// <summary>
    /// similar to console.writeline but with time and stuff. disabled on release builds unless a console flag is added. helpful for debugging.
    /// </summary>
    public static void log(params object[] val)
    {
        /*if (!is_debug() && !verbose_mode) {
            return;
        }*/
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{DateTime.Now}] ");
        Console.ResetColor();

        // print the rest, extra empty line for pretty formatting
        foreach (object obj in val) {
            Console.Write(obj);
        }
        Console.WriteLine("\n");
    }

    /// <summary>
    /// if true, the game is currently running in debug mode
    /// </summary>
    public static bool is_debug()
    {
        bool r = false;
        #if DEBUG
        r = true;
        #endif
        return r;
    }

    /// <summary>
    /// loads an asset (only works with internal files in the assets folder); just a shortened version of AssetManager.load<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T load<T>(string path) where T : IAsset, new()
    {
        return AssetManager.load<T>(path);
    }
}