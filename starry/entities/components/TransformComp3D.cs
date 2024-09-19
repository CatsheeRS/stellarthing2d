using static starry.Starry;

namespace starry;

/// <summary>
/// adds stuff for the isometric game world
/// </summary>
public class WorldTransformComp {
    /// <summary>
    /// position, with 1 being 1 tile, and (0, 0) being the top left
    /// </summary>
    public vec2 position { get; set; } = vec2();
    
    int sideside;
    /// <summary>
    /// the side the thing is pointing at, 0 points bottom, 1 points right, 2 points up, and 3 points left. this will automatically loop if it's smaller than 0 or bigger than 3. 0 is supposed to be the front btw
    /// </summary>
    public int side {
        get { return sideside; }
        set {
            int val = value;
            if (val < 0) val = 0;
            if (val > 3) val = 3;
            sideside = val;
        }
    }

    /// <summary>
    /// if true, the tilemap will treat this block as a floor (no top side)
    /// </summary>
    public bool flat { get; set; } = true;
    /// <summary>
    /// tints the sprite (white is the original colors)
    /// </summary>
    public color tint { get; set; } = color.white;
}