using System.Collections.Generic;

namespace frambos.ecs;

/// <summary>
/// the C in ECS
/// </summary>
public interface IComponent {
    IComponent new_with_defaults();
}

/// <summary>
/// used for attaching systems to entities
/// </summary>
public interface ISystem {
    void create(Entity entity);
    void update(Entity entity, double delta);
    void draw(Entity entity);
}

/// <summary>
/// container for components (data) and systems (actually do stuff)
/// </summary>
public abstract class Entity {
    internal Dictionary<string, object> components { get; set; } = [];
    internal List<ISystem> systems { get; set; } = [];

    /// <summary>
    /// gets or adds a component to the entity
    /// </summary>
    public T get_comp<T>() where T : struct, IComponent
    {
        if (components.TryGetValue(nameof(T), out object value)) {
            return (T)value;
        }
        else {
            components.Add(nameof(T), new T().new_with_defaults());
            return (T)components[nameof(T)];
        }
    }

    public void add_system<T>() where T : ISystem, new()
    {
        systems.Add(new T());
    }
}