using System.Collections.Generic;

namespace frambos.ecs;

/// <summary>
/// the C in ECS
/// </summary>
public interface IComponent {
    /// <summary>
    /// gets a unique string representing the component for highly efficient lookup
    /// </summary>
    string get_key();
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
public class Entity {
    /// <summary>
    /// the name of the entity; no particular reason
    /// </summary>
    public string name { get; set; } = "";
    internal Dictionary<string, object> components { get; set; } = [];
    internal List<ISystem> systems { get; set; } = [];

    public Entity(string name)
    {
        this.name = name;
        EcsManager.entities.Add(this);
    }

    /// <summary>
    /// gets or adds a component to the entity
    /// </summary>
    public T get_comp<T>() where T : IComponent, new()
    {
        T m = new();
        if (components.TryGetValue(m.get_key(), out object value)) {
            return (T)value;
        }
        else {
            components.Add(m.get_key(), m);
            return (T)components[m.get_key()];
        }
    }

    public void add_system<T>() where T : ISystem, new()
    {
        T tee = new();
        systems.Add(tee);
        tee.create(this);
    }
}