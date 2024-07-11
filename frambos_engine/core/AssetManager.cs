using System.Collections.Generic;

namespace frambos.core;

/// <summary>
/// it manages assets :)
/// </summary>
public static class AssetManager {
    /// <summary>
    /// path where the game's assets are
    /// </summary>
    public static string respath { get; set; } = "";
    /// <summary>
    /// we have a dictionary so we don't have to reload stuff, very efficient
    /// </summary>
    static Dictionary<string, IAsset> assets = [];

    /// <summary>
    /// loads an asset (only works with internal files in the assets folder)
    /// </summary>
    public static T load<T>(string path) where T : IAsset, new()
    {
        if (assets.TryGetValue(path, out IAsset value)) {
            return (T)value;
        }
        else {
            // we don't actually load the asset here :D
            Frambos.log("loading asset", $"{respath}/{path}");
            T can_i_get_a_t = new();
            can_i_get_a_t = (T)can_i_get_a_t.load($"{respath}/{path}");
            assets.Add(path, can_i_get_a_t);
            return can_i_get_a_t;
        }
    }
}