using frambos.util;
using frambos.ecs;
using frambos;
using frambos.core;

namespace spacegame.player;

public class PlayerComponent : IComponent
{
    public int speed { get; set; } = 200;
    public Vector2 MyProperty { get; set; }

    public string get_key() => "player";
}

/// <summary>
/// used for drawing textures
/// </summary>
public class Player : ISystem
{
    public void create(Entity e)
    {
        e.add_system<Sprite>();
        e.get_comp<SpriteTexture>().texture = Frambos.load<Texture>("placeholder/player.png");
        e.get_comp<Transform>().size = new Vector2(64, 64);
        e.get_comp<Transform>().center = new Vector2(32, 32);
        e.get_comp<Transform>().position = new Vector2(450, 250);
    }

    public void update(Entity e, double delta)
    {
        FrambosMath.look_at(); // later
        if (InputManager.is_key_pressed(Key.space)) {
            e.get_comp<Transform>().rotation += 100 * delta;
        }
    }

    public void draw(Entity e) {}
}