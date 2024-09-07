using System;
using System.Collections.Generic;
using static starry.Starry;

namespace starry;

/// <summary>
/// manages entities
/// </summary>
public static class World {
    /// <summary>
    /// if true, the game is paused. not all entities get paused, see EntityType
    /// </summary>
    public static bool paused { get; set; } = false;

    internal static HashSet<IEntity> entities { get; set; } = [];
    internal static Dictionary<IEntity, EntityInformation> entityInformation { get; set; } = [];
    internal static Dictionary<string, HashSet<IEntity>> groups { get; set; } = [];

    /// <summary>
    /// adds an entity to the game world
    /// </summary>
    public static void addEntity(IEntity entity)
    {
        entities.Add(entity);
        entityInformation.Add(entity, entity.setup());
        string elgrupo = entity.setup().type switch
        {
            EntityType.gameWorld => "layers.game_world",
            EntityType.ui => "layers.ui",
            EntityType.pauseUi => "layers.pause_ui",
            EntityType.pausableManager => "layers.pausable_manager",
            EntityType.pausedManager => "layers.paused_manager",
            _ => "csharp_stop_complaining",
        };
        addToGroup(elgrupo, entity);

        foreach (string group in entity.setup().groups) {
            addToGroup(group, entity);
        }
    }

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

    public static bool isInGroup(string group, IEntity entity)
    {
        if (!groups.TryGetValue(group, out HashSet<IEntity>? value)) return false;
        return value.Contains(entity);
    }

    internal static void updateEntities()
    {
        spreadToEntities(entity => {
            entity.update(Application.delta);
            return false;
        });
    }

    // return true to stop the spreading
    static void spreadToEntities(Func<IEntity, bool> func)
    {
        // managers run first
        if (paused) {
            foreach (var entity in getGroup("layers.paused_manager")) {
                if (func(entity)) return;
            }
        }
        else {  
            foreach (var entity in getGroup("layers.pausable_manager")) {
                if (func(entity)) return;
            }
        }

        // the ui's next
        if (paused) {
            foreach (var entity in getGroup("layers.pause_ui")) {
                if (func(entity)) return;
            }
        }
        else {
            foreach (var entity in getGroup("layers.ui")) {
                if (func(entity)) return;
            }
        }

        // 3d stuff run last
        if (!paused) {
            foreach (var entity in getGroup("layers.game_world")) {
                if (func(entity)) return;
            }
        }
    }
}