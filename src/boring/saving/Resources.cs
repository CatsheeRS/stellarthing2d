using Godot;

namespace stellarthing;

public static class Resources {
    public static int Oxygen { get; set; } = 0;
    public static int Water { get; set; } = 0;
    public static int RocketFuel { get; set; } = 0;
    public static int Uranium { get; set; } = 0;
    public static int Gunpowder { get; set; } = 0;
    public static int Silicon { get; set; } = 0;
    public static int Iron { get; set; } = 0;
    public static int Steel { get; set; } = 0;
    public static int Titanium { get; set; } = 0;
    public static int Diamond { get; set; } = 0;
    public static int Ruby { get; set; } = 0;
    public static int Emerald { get; set; } = 0;
    public static int Sapphire { get; set; } = 0;
    public static int Obsidian { get; set; } = 0;

    public class SaveVersion {
        public int Oxygen { get; set; } = 0;
        public int Water { get; set; } = 0;
        public int RocketFuel { get; set; } = 0;
        public int Uranium { get; set; } = 0;
        public int Gunpowder { get; set; } = 0;
        public int Silicon { get; set; } = 0;
        public int Iron { get; set; } = 0;
        public int Steel { get; set; } = 0;
        public int Titanium { get; set; } = 0;
        public int Diamond { get; set; } = 0;
        public int Ruby { get; set; } = 0;
        public int Emerald { get; set; } = 0;
        public int Sapphire { get; set; } = 0;
        public int Obsidian { get; set; } = 0;
    }

    public static SaveVersion ToSaveVersion()
    {
        return new SaveVersion {
            Oxygen = Oxygen,
            Water = Water,
            RocketFuel = RocketFuel,
            Uranium = Uranium,
            Gunpowder = Gunpowder,
            Silicon = Silicon,
            Iron = Iron,
            Steel = Steel,
            Titanium = Titanium,
            Diamond = Diamond,
            Ruby =
        };
    }
}
