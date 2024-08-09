using Godot;
using System;

namespace stellarthing;

public partial class AudioStuff : HSlider {
    [Export]
    public string Bus { get; set; } = "";
    bool doingFaffrery = false;

    public override void _Ready() => Hehe();

    public void Hehe()
    {
        if (!doingFaffrery) {
            Config<AudioSettings> config = new();
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = Bus switch {
                "Master" => config.Data.AllSounds,
                "music" => config.Data.Music,
                "ui" => config.Data.UI,
                "ambient_weather" => config.Data.AmbientWeather,
                "enemies" => config.Data.Enemies,
                "furniture" => config.Data.Furniture,
                _ => -1
            };
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;

            // actually change the bloody volume bollocks mate
            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));
        }
    }

    public void OnDragStart() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        Config<AudioSettings> config = new();
        switch (Bus) {
            case "Master": config.Data.AllSounds = (int)Value; break;
            case "music": config.Data.Music = (int)Value; break;
            case "ui": config.Data.UI = (int)Value; break;
            case "ambient_weather": config.Data.AmbientWeather = (int)Value; break;
            case "enemies": config.Data.Enemies = (int)Value; break;
            case "furniture": config.Data.Furniture = (int)Value; break;
        }
        config.Save();

        // actually change the bloody volume bollocks mate
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));

        doingFaffrery = false;
    }
}
