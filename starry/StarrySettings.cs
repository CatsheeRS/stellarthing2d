namespace starry;

public struct StarrySettings
{
    public StarrySettings() {}

    /// <summary>
    /// the game name, used for the window title and stuff
    /// </summary>
    public string gameName { get; set; } = "Starry Engine";
    /// <summary>
    /// the game's version. should start with a lowercase V.
    /// </summary>
    public string gameVersion { get; set; } = "v0.0.0";
    /// <summary>
    /// if true, the engine will display all logging, which is probably bad for performance since json
    /// </summary>
    public bool verbose { get; set; } = false;
}