using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace starry;

/// <summary>
/// assets. please note assets are stored in a dictionary so they're only loaded once and only deleted at the end of the game.
/// </summary>
public static class Assets {
    internal static Dictionary<string, IAsset> assets = [];

    /// <summary>
    /// loads the assets and then puts it in a handsome dictionary of stuff so its blazingly fast or smth idfk
    /// </summary>
    public static async Task<T> load<T>(string path) where T: IAsset, new()
    {
        return await Task.Run(() => {
            if (assets.ContainsKey(path)) {
                return (T)assets[path];
            }
            else {
                T tee = new();
                tee.load(Path.Combine(Starry.settings.assetPath, path));
                assets.Add(path, tee);
                return tee;
            }
        });
    }

    public static void cleanup()
    {
        foreach (var asse in assets) {
            Starry.log($"Deleting {asse.Key}");
            asse.Value.cleanup();
        }
        assets.Clear();
    }
}