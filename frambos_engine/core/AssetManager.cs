using System.Collections.Generic;

namespace frambos.core;

public static class AssetManager {
    /// <summary>
    /// path where the game's assets are
    /// </summary>
    public static string respath { get; set; } = "";
    /// <summary>
    /// we have a dictionary so we don't have to reload stuff, very efficient
    /// </summary>
    static Dictionary<string, IAsset> resources = [];

    public static T load<T>(string path) where T : IAsset, new() {
        if (resources.TryGetValue(path, out IAsset value)) {
            return (T)value;
        }
        else {
            // we don't actually load the resource here :D

        }
    }
}