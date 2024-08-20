using Godot;
using System;

namespace stellarthing;

public partial class MouseSensitivitySlider : HSlider
{
	bool doingFaffrery = false;

    public override void _Ready() => Hehe();

    public void Hehe()
    {
        if (!doingFaffrery) {
            int partofaseriesondiscriminationfromwikipediathefreeencyclopedia = 
                Stellarthing.GetConfig<int>(ConfigSections.Controls, ConfigKeys.ControlsMouseSensitivity, 7);
            Value = partofaseriesondiscriminationfromwikipediathefreeencyclopedia;

            // bloody hell mate
			if (Player.Camera != null) {
				Player.Camera.yawSensitivity = Value / 100;
				Player.Camera.pitchSensitivity = Value / 100;
			}
        }
    }

    public void OnDragStarted() => doingFaffrery = true;

    public void OnDragEnd(bool valueChanged)
    {
        if (!valueChanged) return;

        Stellarthing.SetConfig(ConfigSections.Controls, ConfigKeys.ControlsMouseSensitivity, Value);
        if (Player.Camera != null) {
			Player.Camera.yawSensitivity = Value / 100;
			Player.Camera.pitchSensitivity = Value / 100;
		}
        doingFaffrery = false;
    }
}
