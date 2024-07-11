using frambos.core;
using frambos.graphics;
using frambos.util;

namespace frambos.ecs;

/// <summary>
/// used for drawing textures
/// </summary>
class Sprite : ISystem
{
    public void create(Entity entity) {}
    public void update(Entity entity, double delta) {}

    public void draw(Entity entity)
    {
        var tf = entity.get_comp<Transform>();
        var spr = entity.get_comp<SpriteTexture>();

        if (!tf.visible || spr.texture == null) {
            return;
        }

        Renderer.draw_texture(
            spr.texture, new Vector2(tf.position.x, tf.position.y), tf.size, tf.rotation,
            tf.center, spr.flip_x, spr.flip_y
        );
    }
}