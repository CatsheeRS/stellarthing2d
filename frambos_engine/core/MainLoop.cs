using System;

namespace frambos.core;

public class MainLoop {
    /// <summary>
    /// startups the engine
    /// </summary>
    public MainLoop(string[] args)
    {
        // unrequested help command
        Console.WriteLine("Options:");
        Console.WriteLine("  -v, --verbose: shows error messages, helpful for troubleshooting");

        // first check arguments for the engine
        foreach (var arg in args) {
            if (arg == "-v" || arg == "--verbose") {
                Frambos.verbose_mode = true;
            }
        }

        Frambos.log("starting up engine");
    }
}
