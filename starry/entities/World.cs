using System.Collections.Generic;
using Silk.NET.GLFW;
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
    static double prevtime { get; set; }
    static Glfw? glfw;
    
    internal static void create(Glfw glfw)
    {
        World.glfw = glfw;
        prevtime = glfw.GetTime();
    }

    /// <summary>
    /// adds an entity to the game world
    /// </summary>
    public static void addEntity(IEntity entity)
    {
        entities.Add(entity);
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
        // get delta :D
        double delta = (glfw?.GetTime() ?? 0) - prevtime;
        prevtime = glfw?.GetTime() ?? 0;

        // managers run first
        if (paused) {
            foreach (var entity in getGroup("layers.paused_manager")) {
                entity.update(delta);
            }
        }
        else {
            foreach (var entity in getGroup("layers.pausable_manager")) {
                entity.update(delta);
            }
        }

        // the ui's next
        if (paused) {
            foreach (var entity in getGroup("layers.pause_ui")) {
                entity.update(delta);
            }
        }
        else {
            foreach (var entity in getGroup("layers.ui")) {
                entity.update(delta);
            }
        }

        // 3d stuff run last
        if (!paused) {
            foreach (var entity in getGroup("layers.world")) {
                entity.update(delta);
            }
        }
    }
}