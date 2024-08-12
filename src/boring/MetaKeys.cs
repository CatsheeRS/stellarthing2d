namespace stellarthing;

/// <summary>
/// a bunch of metadata keys so I can have autocomplete
/// </summary>
public static class MetaKeys
{
    /// <summary>
    /// when in a button, tells the game to not play the button you hear when clicking button
    /// </summary>
    public static string DontPlayButtonSound { get => "dont_play_button_sound"; }
    /// <summary>
    /// string value for specifying the key of the furniture, so it can be traced back to the inventory
    /// </summary>
    public static string FurnitureKey { get => "furniture_key"; }
    /// <summary>
    /// json for the item this furniture was made from
    /// </summary>
    public static string FurnitureItem { get => "furniture_item"; }
}