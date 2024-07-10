using System;

namespace frambos;

/// <summary>
/// many general-purpose static functions and properties
/// </summary>
public static class Frambos
{
    public static bool verbose_mode { get; internal set; } = false;

    /// <summary>
    /// similar to console.writeline but with time and a simple stack trace. disabled on release builds unless a console flag is added. helpful for debugging.
    /// </summary>
    /// <param name="val"></param>
    public static void log(params object[] val)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{DateTime.Now}] ");

        // print the rest, extra empty line for pretty formatting
        foreach (object obj in val) {
            Console.Write(obj);
        }
        Console.WriteLine("\n");
    }

    /// <summary>
    /// if true, the game is currently running in debug mode
    /// </summary>
    /// <returns></returns>
    public static bool is_debug()
    {
        bool r = false;
        #if DEBUG
        r = true;
        #endif
        return r;
    }
}