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
            ConfigFile config = new();
            config.Load("user://prefs.cfg");
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = (int)config.GetValue("audio", Bus, 100);
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;

            // actually change the bloody volume bollocks mate
            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));
        }
    }

    public void OnDragStart() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        ConfigFile config = new();
        config.Load("user://prefs.cfg");
        config.SetValue("audio", Bus, Value);
        config.Save("user://prefs.cfg");

        // actually change the bloody volume bollocks mate
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));

        doingFaffrery = false;
    }
}