using System.Collections.Generic;

namespace frambos.ecs;

public static class EcsManager
{
    /// <summary>
    /// simple way of organizing entities for later use
    /// </summary>
    public static Dictionary<string, List<Entity>> groups { get; set; } = [];
    /// <summary>
    /// every single entity ever
    /// </summary>
    internal static List<Entity> entities { get; set; } = [];

    internal static void update_everything(double delta)
    {
        foreach (Entity entity in entities) {
            foreach (ISystem system in entity.systems) {
                system.update(entity, delta);
            }
        }
    }

    internal static void render_everything()
    {
        foreach (Entity entity in entities) {
            foreach (ISystem system in entity.systems) {
                system.draw(entity);
            }
        }
    }
}