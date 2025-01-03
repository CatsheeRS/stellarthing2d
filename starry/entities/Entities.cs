using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
namespace starry;

/// <summary>
/// it manages entities
/// </summary>
public static class Entities {
    /// <summary>
    /// entities of the game world type
    /// </summary>
    public const string GAME_WORLD_GROUP = "starry.Entities.WORLD_GROUP";
    /// <summary>
    /// entities of the ui type
    /// </summary>
    public const string UI_GROUP = "starry.Entities.UI_GROUP";
    /// <summary>
    /// entities of the paused ui type
    /// </summary>
    public const string PAUSED_UI_GROUP = "starry.Entities.PAUSED_UI_GROUP";
    /// <summary>
    /// entities of the manager type
    /// </summary>
    public const string MANAGER_GROUP = "starry.Entities.MANAGER_GROUP";
    /// <summary>
    /// entities of the paused manager type
    /// </summary>
    public const string PAUSED_MANAGER_GROUP = "starry.Entities.PAUSED_MANAGER_GROUP";

    /// <summary>
    /// if true, the game is paused. not all entities get paused, see EntityType
    /// </summary>
    public static bool paused { get; set; } = false;

    public static ConcurrentHashSet<IEntity> entities { get; internal set; } = [];
    public static ConcurrentDictionary<string, ConcurrentHashSet<IEntity>> groups { get; internal set; } = new();
    public static ConcurrentDictionary<IEntity, ConcurrentHashSet<IComponent>> components { get; internal set; } = new();

    /// <summary>
    /// adds an entity.
    /// </summary>
    public static void addEntity(IEntity entity)
    {
        entities.Add(entity);
        string elgrupo = entity.entityType switch {
            EntityType.gameWorld => GAME_WORLD_GROUP,
            EntityType.ui => UI_GROUP,
            EntityType.pausedUi => PAUSED_UI_GROUP,
            EntityType.manager => MANAGER_GROUP,
            EntityType.pausedManager => PAUSED_MANAGER_GROUP,
            _ => throw new Exception("man"),
        };
        addToGroup(elgrupo, entity);

        string[] losgrupos = entity.initGroups;
        foreach (string group in losgrupos) {
            addToGroup(group, entity);
        }

        entity.create();
        components.TryAdd(entity, []);
    }

    /// <summary>
    /// gets the group, and creates the group if it doesn't exist yet
    /// </summary>
    public static ConcurrentHashSet<IEntity> getGroup(string group) => groups.GetOrAdd(group, []);

    /// <summary>
    /// adds an entity to the group, and creates the group if it doesn't exist yet
    /// </summary>
    public static void addToGroup(string group, IEntity entity)
    {
        var jjj = groups.GetOrAdd(group, []);
        jjj.Add(entity);
    }

    /// <summary>
    /// if true, the entity is in that group
    /// </summary>
    public static bool isInGroup(string group, IEntity entity)
    {
        if (!groups.ContainsKey(group)) return false;
        return groups[group].Contains(entity);
    }
    
    /// <summary>
    /// updates entities duh
    /// </summary>
    public static async Task update()
    {
        static void mate(IEntity entity) {
            entity.update(Window.deltaTime);
            entity.draw();
            foreach (IComponent component in components[entity]) {
                component.update(entity, Window.deltaTime);
                component.draw(entity);
            }
        }

        if (paused) {
            // i know
            await Parallel.ForEachAsync(getGroup(PAUSED_MANAGER_GROUP), async (entity, ct) => {
                await Task.Run(() => mate(entity));
            });

            await Parallel.ForEachAsync(getGroup(PAUSED_UI_GROUP), async (entity, ct) => {
                await Task.Run(() => mate(entity));
            });
        }
        else {
            await Parallel.ForEachAsync(getGroup(MANAGER_GROUP), async (entity, ct) => {
                await Task.Run(() => mate(entity));
            });

            await Parallel.ForEachAsync(getGroup(UI_GROUP), async (entity, ct) => {
                await Task.Run(() => mate(entity));
            });
            
            await Parallel.ForEachAsync(getGroup(GAME_WORLD_GROUP), async (entity, ct) => {
                await Task.Run(() => mate(entity));
            });
        }
    }

    /// <summary>
    /// it adds a component to the entity, and returns the added component
    /// </summary>
    public static T addComponent<T>(IEntity entity) where T: class, IComponent, new()
    {
        T tee = new();
        components[entity].Add(tee);
        tee.create(entity);
        return tee;
    }
    
    /// <summary>
    /// if true, the entity has that component
    /// </summary>
    public static bool hasComponent<T>(IEntity entity) where T: class, IComponent, new() =>
        components[entity].OfType<T>().Any();

    /// <summary>
    /// gets component or adds it if it's not there
    /// </summary>
    public static T getComponent<T>(IEntity entity) where T: class, IComponent, new()
    {
        if (hasComponent<T>(entity)) return components[entity].OfType<T>().First();
        
        addComponent<T>(entity);
        return components[entity].OfType<T>().First();
    }
}