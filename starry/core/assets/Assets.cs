using System;
using System.Collections.Generic;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages all things assets
/// </summary>
public static class Assets {
    static Dictionary<string, IAsset> assets = [];

    /// <summary>
    /// loads an asset, with the path specified in the project settings. for efficiency, assets are loaded once and then put into a dictionary with the paths.
    /// </summary>
    public static T load<T>(string path) where T : IAsset, new()
    {
        if (!assets.TryGetValue(path, out IAsset? value)) {
            T canigetaT = new();
            canigetaT.load($"{settings.assetPath}/{path}");
            assets.Add(path, canigetaT);
            return canigetaT;
        }
        else {
            return (T)value;
        }
    }
}

/// <summary>
/// base of all assets
/// </summary>
public interface IAsset {
    /// <summary>
    /// specifies how is the asset loaded
    /// </summary>
    public void load(string path);
}