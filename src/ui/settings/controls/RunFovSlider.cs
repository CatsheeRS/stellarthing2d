using Godot;
using System;

namespace stellarthing;

public partial class RunFovSlider : HSlider
{
	bool doingFaffrery = false;

    public override void _Ready() => Hehe();

    public void Hehe()
    {
        if (!doingFaffrery) {
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = 
                Stellarthing.GetConfig<int>(ConfigSections.Controls, ConfigKeys.ControlsRunFov, 105);
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;

            // bloody hell mate
			if (Player.Camera != null) Player.Camera.RunFov = (float)Value;
        }
    }

    public void OnDragStarted() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        Stellarthing.SetConfig(ConfigSections.Controls, ConfigKeys.ControlsRunFov, Value);
        if (Player.Camera != null) Player.Camera.RunFov = (float)Value;
        doingFaffrery = false;
    }
}
