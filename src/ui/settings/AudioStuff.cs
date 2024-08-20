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
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = 
                Stellarthing.GetConfig<int>(ConfigSections.Audio, Bus, 100);
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;      

            // actually change the bloody volume bollocks mate
            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));
        }
    }

    public void OnDragStarted() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        Stellarthing.SetConfig(ConfigSections.Audio, Bus, Value);
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(Bus), (float)Mathf.LinearToDb(Value / 100f));
        doingFaffrery = false;
    }
}