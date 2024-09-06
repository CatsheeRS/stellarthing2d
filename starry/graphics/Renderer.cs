using System;
using Veldrid;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things rendering
/// </summary>
public static class Renderer {
    static GraphicsDevice? vd;

    /// <summary>
    /// sets up rendering
    /// </summary>
    public static void create(GraphicsDevice device)
    {
        vd = device;
    }
}