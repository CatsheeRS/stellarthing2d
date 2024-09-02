using static starry.Starry;

namespace starry;

/// <summary>
/// base of all components
/// </summary>
public interface IComponent {
    /// <summary>
    /// called every frame. delta is in seconds
    /// </summary>
    public void update(double delta) {}
    /// <summary>
    /// when the entity receives input
    /// </summary>
    public void input() {}
}