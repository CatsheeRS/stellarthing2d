using Godot;
using System;

namespace stellarthing;

public partial class FovSlider : HSlider
{
	bool doingFaffrery = false;

    public override void _Ready() => Hehe();

    public void Hehe()
    {
        if (!doingFaffrery) {
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = 
                Stellarthing.GetConfig<int>(ConfigSections.Controls, ConfigKeys.ControlsFov, 90);
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;      

            // bloody hell mate
			if (Player.Camera != null) Player.Camera.NormalFov = (float)Value;
        }
    }

    public void OnDragStarted() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        Stellarthing.SetConfig(ConfigSections.Controls, ConfigKeys.ControlsFov, Value);
        if (Player.Camera != null) Player.Camera.NormalFov = (float)Value;
        doingFaffrery = false;
    }
}
