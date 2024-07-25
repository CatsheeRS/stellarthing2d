using Godot;
using System;

namespace stellarthing;

public partial class ResourceDisplayFaffery : Node {
	[Export]
	public Label Oxygen { get; set; }
	[Export]
	public Label RocketFuel { get; set; }
	[Export]
	public Label Uranium { get; set; }
	[Export]
	public Label Gunpowder { get; set; }
	[Export]
	public Label Silicon { get; set; }
	[Export]
	public Label Iron { get; set; }
	[Export]
	public Label Steel { get; set; }
	[Export]
	public Label Titanium { get; set; }
	[Export]
	public Label Diamond { get; set; }
	[Export]
	public Label Ruby { get; set; }
	[Export]
	public Label Emerald { get; set; }
	[Export]
	public Label Sapphire { get; set; }
	[Export]
	public Label Obsidian { get; set; }

	public void LoadStuff()
	{
		Config<Resources> config = new();
		Oxygen.Text = $"{Tr("Oxygen:")} {config.Data.Oxygen}";
		RocketFuel.Text = $"{Tr("Rocket fuel:")} {config.Data.RocketFuel}";
		Uranium.Text = $"{Tr("Uranium:")} {config.Data.Uranium}";
		Gunpowder.Text = $"{Tr("Gunpowder:")} {config.Data.Gunpowder}";
		Silicon.Text = $"{Tr("Silicon:")} {config.Data.Silicon}";
		Iron.Text = $"{Tr("Iron:")} {config.Data.Iron}";
		Steel.Text = $"{Tr("Steel:")} {config.Data.Steel}";
		Titanium.Text = $"{Tr("Titanium:")} {config.Data.Titanium}";
		Diamond.Text = $"{Tr("Diamond:")} {config.Data.Diamond}";
		Ruby.Text = $"{Tr("Ruby:")} {config.Data.Ruby}";
		Emerald.Text = $"{Tr("Emerald:")} {config.Data.Emerald}";
		Sapphire.Text = $"{Tr("Sapphire:")} {config.Data.Sapphire}";
		Obsidian.Text = $"{Tr("Obsidian:")} {config.Data.Obsidian}";
	}
}
