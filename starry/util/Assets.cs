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
            string locamiño = Path.Combine(Starry.settings.assetPath, path);
            if (assets.ContainsKey(locamiño)) {
                return (T)assets[locamiño];
            }
            else {
                T tee = new();
                tee.load(Path.Combine(Starry.settings.assetPath, path));
                return tee;
            }
        });
    }

    public static async Task cleanup()
    {
        await Task.Run(() => {
            foreach (var asse in assets) {
                asse.Value.cleanup();
            }
            assets.Clear();
        });
    }
}