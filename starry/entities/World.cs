using System.Collections.Generic;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages entities
/// </summary>
public static class World {
    static HashSet<IEntity> entities { get; set; } = [];
    static Dictionary<IEntity, EntityInformation> entityInformation { get; set; } = [];
    static Dictionary<string, HashSet<IEntity>> groups { get; set; } = [];

    /// <summary>
    /// adds an entity to the game world
    /// </summary>
    public static void addEntity(IEntity entity) => entities.Add(entity);

    /// <summary>
    /// gets every entity in a group
    /// </summary>
    public static HashSet<IEntity> getGroup(string name)
    {
        if (groups.TryGetValue(name, out HashSet<IEntity>? value)) {
            return value;
        }
        else {
            groups.Add(name, []);
            return [];
        }
    }

    /// <summary>
    /// adds an entity to a group, and creates the group if it doesn't exist yet
    /// </summary>
    public static void addToGroup(string group, IEntity entity)
    {
        if (groups.TryGetValue(group, out HashSet<IEntity>? value)) {
            value.Add(entity);
        }
        else {
            groups.Add(group, [entity]);
        }
    }
}