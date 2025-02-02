using System;
namespace starry;

/// <summary>
/// its an entity lmao
/// </summary>
public abstract class IEntity
{
    /// <summary>
    /// ran when the bloody entity is bloody created bloody hell mate. this is ran right after the entity is added to the entities stuffs
    /// </summary>
    public virtual void create() {}

    /// <summary>
    /// ran every frame
    /// </summary
    public virtual void update(double delta) {}

    /// <summary>
    /// ran every frame (if server)
    /// </summary
    public void serverUpdate(double delta) {}

    /// <summary>
    /// ran every frame (if client)
    /// </summary
    public void clientUpdate(double delta) {}

    /// <summary>
    /// also ran every frame but youre supposed to draw stuff here
    /// </summary>
    public virtual void draw() {}

    /// <summary>
    /// returns the entity type what did you expect
    /// </summary>
    public abstract EntityType entityType { get; }

    /// <summary>
    /// returns the name what did you expect. idk why this exists but the game's actual behavior shouldn't depend on this
    /// </summary>
    public abstract string name { get; }

    /// <summary>
    /// groups the entity is assigned to when created. you should use constants so there's any ide support ever
    /// </summary>
    public abstract string[] initGroups { get; }
    
    /// <summary>
    /// state of the entity
    /// </summary>
    public EntityState state = EntityState.BIRTHING;
}

/// <summary>
/// the circle of life
/// </summary>
public enum EntityState
{
    BIRTHING,
    LIVING,
    DYING
}

/// <summary>
/// type of the entity; affects rendering, input, and pausing. update functions are called in this order: pausable/paused manager -> pausable/pause ui -> always running -> game world
/// </summary>
public enum EntityType {
    /// <summary>
    /// pausable 3d-ish object
    /// </summary>
    GAME_WORLD,
    /// <summary>
    /// pausable 2d interface
    /// </summary>
    UI,
    /// <summary>
    /// 2d interface only ran when paused
    /// </summary>
    PAUSED_UI,
    /// <summary>
    /// entity that isn't rendered or interacted with (doesn't receive input) and just manages stuff, can be paused
    /// </summary>
    MANAGER,
    /// <summary>
    /// entity that isn't rendered or interacted with (doesn't receive input) and just manages stuff, only rendered on pause
    /// </summary>
    PAUSED_MANAGER,
    /// <summary>
    /// similar to <c>gameWorld</c> but it's always running no matter the chunk or world. convenient for factories, as forcing players to fit factories in a single chunk and then stay there for the factory to work would be very stupid. note that this actually can be paused, so it's not always running
    /// </summary>
    ALWAYS_RUNNING,
}