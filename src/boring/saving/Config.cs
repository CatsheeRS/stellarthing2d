using Godot;
using Newtonsoft.Json;
using System;

namespace stellarthing;

/// <summary>
/// a container for data or some shit like that.
/// </summary>
public class Config<T> where T : IConfigData, new() {
    /// <summary>
    /// the data in this config.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// saves the config and shit.
    /// </summary>
    public void Save()
    {
        using var file = FileAccess.Open(FigureOutTheFuckingPath(Data.GetFilename()), FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            })
        );
    }

    /// <summary>
    /// it loads some bullshit, or creates a new one if it doesn't exist yet.
    /// </summary>
    public Config()
    {
        // temporary object so we can generate a path and load the data frfrfr
        T tempdata = new();
        string path = FigureOutTheFuckingPath(tempdata.GetFilename());

        if (FileAccess.FileExists(path)) {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            Data = JsonConvert.DeserializeObject<T>(
                file.GetAsText(), new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                }
            );
            file.Close();
        }
        else {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(tempdata, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                })
            );
            file.Close();
            Data = tempdata;
        }
    }

    static string FigureOutTheFuckingPath(string originalPath)
    {
        string m = "user://" + originalPath.Replace("%universe", $"universes/{Stellarthing.CurrentUniverse}");
        DirAccess.MakeDirRecursiveAbsolute(m.GetBaseDir());
        return m;
    }
}

public interface IConfigData {
    string GetFilename();
}