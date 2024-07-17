using Godot;
using System;

namespace stellarthing;

/// <summary>
/// handsome singleton for crap
/// </summary>
public partial class Stellarthing : Node {
	/// <summary>
	/// name of the current universe, used for saving
	/// </summary>
	public static string CurrentUniverse { get; set; } = "";
}
