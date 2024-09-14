using System;
using System.Collections.Generic;
using System.Numerics;
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
        Renderer.renderUi();
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
        Renderer.renderWorld();
        if (!paused) {
            foreach (var entity in getGroup("layers.game_world")) {
                if (func(entity)) return;
            }
        }
    }

    /// <summary>
    /// the position of the 3d camera
    /// </summary>
    /*public static vec3 cameraPosition {
        get => vec3(Renderer.camera3d.Position.X, Renderer.camera3d.Position.Y, Renderer.camera3d.Position.Z);
        set => Renderer.camera3d.Position = new Vector3((float)value.x, (float)value.y, (float)value.z);
    }

    /// <summary>
    /// the position the camera is pointing towards
    /// </summary>
    public static vec3 cameraTarget {
        get => vec3(Renderer.camera3d.Target.X, Renderer.camera3d.Target.Y, Renderer.camera3d.Target.Z);
        set => Renderer.camera3d.Target = new Vector3((float)value.x, (float)value.y, (float)value.z);
    }
    
    /// <summary>
    /// true will switch to perspective, false will switch to orthographic. the difference is that in a perspective projection, things get smaller as they get further away, which is how we naturally see. meanwhile, an orthographic projection doesn't, which is great for isometric cameras and rendering 2d if you want to do that in 3d for some reason
    /// </summary>
    public static bool cameraPerspective {
        get => Renderer.camera3d.Projection == Raylib_cs.CameraProjection.Perspective;
        set => Renderer.camera3d.Projection = value ? Raylib_cs.CameraProjection.Perspective :
            Raylib_cs.CameraProjection.Orthographic;
    }

    /// <summary>
    /// the field-of-view of the camera, from 30 to 150, default is 90
    /// </summary>
    public static double cameraFov {
        get {
            float elmierda = Renderer.camera3d.FovY;
            float RADICAL = elmierda * (MathF.PI / 180f);
            // glad that twitter was banned in brazil
            float RATIO = settings.renderSize.x / settings.renderSize.y;
            float HADICAL = 2f * MathF.Atan(MathF.Tan(RADICAL / 2f) * RATIO);
            return HADICAL * (180f / MathF.PI);
        }
        set {
            float hfov = (float)(value * (Math.PI / 180));
            // glad that twitter was banned in brazil
            float RATIO = settings.renderSize.x / settings.renderSize.y;
            Renderer.camera3d.FovY = 2f * MathF.Atan(MathF.Tan(hfov / 2f) * RATIO) * (180f / MathF.PI);
        }
    }*/
}