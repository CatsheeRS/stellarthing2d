using Godot;

namespace stellarthing;

public class MoneyTime : IConfigData {
    public string GetFilename() => "%universe/money_time.json";
    public uint Year { get; set; } = 2371;
    public uint Month { get; set; } = 2;
    public uint Day { get; set; } = 15;
    public uint Hour { get; set; } = 13;
    public decimal Money { get; set; } = 0.00m;
}
