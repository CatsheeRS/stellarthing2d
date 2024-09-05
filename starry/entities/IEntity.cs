using static starry.Starry;

namespace starry;

/// <summary>
/// base of all entitites
/// </summary>
public interface IEntity {
    /// <summary>
    /// gets entity information, required for all entities
    /// </summary>
    public EntityInformation setup();
    /// <summary>
    /// called every frame. delta is in seconds
    /// </summary>
    public void update(double delta) {}
    /// <summary>
    /// when the entity receives input. must return whether or not the input was handled, if true, the input will stop spreading.
    /// </summary>
    public bool input(IInputEvent @event) => false;
}