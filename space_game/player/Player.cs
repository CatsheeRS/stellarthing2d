using frambos.util;
using frambos.ecs;
using frambos;
using frambos.core;

namespace spacegame.player;

/// <summary>
/// used for drawing textures
/// </summary>
public class Player : ISystem
{
    public void create(Entity entity)
    {
        entity.add_system<Sprite>();
        entity.get_comp<SpriteTexture>().texture = Frambos.load<Texture>("ben.png");
        entity.get_comp<Transform>().size = new Vector2(128, 128);
        entity.get_comp<Transform>().center = new Vector2(64, 64);
        entity.get_comp<Transform>().position = new Vector3(450, 250, 0);
    }

    public void update(Entity entity, double delta)
    {
        if (InputManager.is_key_pressed(Key.space)) {
            entity.get_comp<Transform>().rotation += 25 * delta;
        }
    }

    public void draw(Entity entity) {}
}