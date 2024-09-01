namespace starry;

public struct StarrySettings
{
    public StarrySettings() {}

    /// <summary>
    /// the game's version. should start with a lowercase V.
    /// </summary>
    public string gameVersion { get; set; } = "v0.0.0";
}