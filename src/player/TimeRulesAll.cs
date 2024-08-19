using Godot;
using System;

namespace stellarthing;

public partial class TimeRulesAll : Label
{
    public override void _Ready()
    {
        Text = $"{Universe.Year}-{Universe.Month:D2}-{Universe.Day:D2} {Universe.Hour:D2}:00";
    }

    public void HourThing()
	{
		Universe.Hour++;

		if (Universe.Hour > 23) {
			Universe.Day++;
			Universe.Hour = 0;
		}

		if (Universe.Day > 30) {
			Universe.Month++;
			Universe.Day = 1;
		}

		if (Universe.Month > 12) {
			Universe.Year++;
			Universe.Month = 1;
		}

		Text = $"{Universe.Year}-{Universe.Month:D2}-{Universe.Day:D2} {Universe.Hour:D2}:00";
	}
}
