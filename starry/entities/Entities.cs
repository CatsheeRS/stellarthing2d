using System;
using System.Collections.Concurrent;
using System.Threading;
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
    [BobIgnore]
    public static bool paused { get; set; } = false;

    public static ConcurrentHashSet<IEntity> entities { get; set; }= [];
    public static ConcurrentDictionary<string, ConcurrentHashSet<IEntity>> groups { get; set; } = [];

    /// <summary>
    /// adds an entity.
    /// </summary>
    public static void addEntity(IEntity entity)
    {
        entities.Add(entity);
        string elgrupo = entity.getEntityType() switch {
            EntityType.gameWorld => GAME_WORLD_GROUP,
            EntityType.ui => UI_GROUP,
            EntityType.pausedUi => PAUSED_UI_GROUP,
            EntityType.manager => MANAGER_GROUP,
            EntityType.pausedManager => PAUSED_MANAGER_GROUP,
            _ => throw new Exception("man"),
        };
        addToGroup(elgrupo, entity);

        string[] losgrupos = entity.getInitGroups();
        foreach (string group in losgrupos) {
            addToGroup(group, entity);
        }

        entity.create();
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
        if (paused) {
            // i know
            await Parallel.ForEachAsync(getGroup(PAUSED_MANAGER_GROUP), async (entity, ct) => {
                await Task.Run(() => entity.update(Window.deltaTime), CancellationToken.None);
                //await Task.Run(entity.draw, CancellationToken.None);
            });

            await Parallel.ForEachAsync(getGroup(PAUSED_UI_GROUP), async (entity, ct) => {
                await Task.Run(() => entity.update(Window.deltaTime), CancellationToken.None);
                await Task.Run(entity.draw, CancellationToken.None);
            });
        }
        else {
            await Parallel.ForEachAsync(getGroup(MANAGER_GROUP), async (entity, ct) => {
                await Task.Run(() => entity.update(Window.deltaTime), CancellationToken.None);
                //await Task.Run(entity.draw, CancellationToken.None);
            });

            await Parallel.ForEachAsync(getGroup(UI_GROUP), async (entity, ct) => {
                await Task.Run(() => entity.update(Window.deltaTime), CancellationToken.None);
                await Task.Run(entity.draw, CancellationToken.None);
            });
            
            await Parallel.ForEachAsync(getGroup(GAME_WORLD_GROUP), async (entity, ct) => {
                await Task.Run(() => entity.update(Window.deltaTime), CancellationToken.None);
                await Task.Run(entity.draw, CancellationToken.None);
            });
        }
    }
}