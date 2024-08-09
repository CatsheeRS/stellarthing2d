using System;
using Godot;

namespace stellarthing;

public class AudioSettings : IConfigData {
    public string GetFilename() => "audio.json";
    public int AllSounds { get; set; } = 100;
    public int Music { get; set; } = 100;
    public int UI { get; set; } = 100;
    public int AmbientWeather { get; set; } = 100;
    public int Enemies { get; set; } = 100;
    public int Furniture { get; set; } = 100;
}