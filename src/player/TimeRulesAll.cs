using Godot;
using System;

namespace stellarthing;

public partial class TimeRulesAll : Label {
	Config<MoneyTime> config = new();

    public override void _Ready()
    {
        Text = $"{config.Data.Year}-{config.Data.Month:D2}-{config.Data.Day:D2} {config.Data.Hour:D2}:00";
    }

    public void HourThing()
	{
		config.Data.Hour++;

		if (config.Data.Hour > 23) {
			config.Data.Day++;
			config.Data.Hour = 0;
		}

		if (config.Data.Day > 30) {
			config.Data.Month++;
			config.Data.Day = 1;
		}

		if (config.Data.Month > 12) {
			config.Data.Year++;
			config.Data.Month = 1;
		}

		config.Save();
		Text = $"{config.Data.Year}-{config.Data.Month:D2}-{config.Data.Day:D2} {config.Data.Hour:D2}:00";
	}
}
