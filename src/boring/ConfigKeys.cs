namespace stellarthing;

/// <summary>
/// a bunch of config keys so I can have autocomplete
/// </summary>
public static class ConfigKeys
{
    /// <summary>
    /// all sounds
    /// </summary>
    public static string AudioMaster { get => "Master"; }
    /// <summary>
    /// the music of the game
    /// </summary>
    public static string AudioMusic { get => "Music"; }
    /// <summary>
    /// the noises of the environment
    /// </summary>
    public static string AudioAmbientWeather { get => "ambient_weather"; }
    /// <summary>
    /// anyone attacking you
    /// </summary>
    public static string AudioEnemies { get => "enemies"; }
    /// <summary>
    /// the user interface
    /// </summary>
    public static string AudioUserInterface { get => "ui"; }
    /// <summary>
    /// sounds of furniture doing stuff
    /// </summary>
    public static string AudioFurniture { get => "furniture"; }

    /// <summary>
    /// the fov of the camera
    /// </summary>
    public static string ControlsFov { get => "fov"; }
    /// <summary>
    /// the fov of the camera while running, usually higher than the normal fov
    /// </summary>
    public static string ControlsRunFov { get => "run_fov"; }
    /// <summary>
    /// the mouse sensitivity :D (from 1 to 10)
    /// </summary>
    public static string ControlsMouseSensitivity { get => "mouse_sensitivity"; }
}