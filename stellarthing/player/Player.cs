using System;
using starry;
using static starry.Starry;

namespace stellarthing;

public class Player : IEntity
{
    public EntityInformation setup() => new() {
        type = EntityType.gameWorld,
        tag = "player",
    };

    Sprite spr = load<Sprite>("red_ball.png");
    double speed = 350;

    TransformComp3D tf = new() {
        position = vec3(0, 0, 0),
        scale = vec2(15 , 10),
    };

    public Player() {}

    public void update(double delta)
    {
        // move :D
        // TODO: add a physics component for managing velocity and collisions (something like ps.velocity with
        // a setter to apply collisions)
        vec2 velocity = vec2();
        if (Input.isKeymapPressed("move_up")) velocity -= vec2(0, 1);
        if (Input.isKeymapPressed("move_down")) velocity += vec2(0, 1);
        if (Input.isKeymapPressed("move_left")) velocity -= vec2(1, 0);
        if (Input.isKeymapPressed("move_right")) velocity += vec2(1, 0);
        vec2 dir = velocity;
        velocity = velocity.normalized() * vec2(speed * delta, speed * delta);
        tf.position += velocity.as3d(0);

        // rotate stuff
        // yes this is all hardcoded and there's nothing you can do about it
        /*tf.rotation = dir switch {
            vec2(0, 1) => 0,
            vec2(0, -1) => 180,
            vec2(1, 0) => -90,
            vec2(-1, 0) => 90,
            vec2(1, -1) => -135,
            vec2(-1, -1) => 135,
            vec2(-1, 1) => 45,
            vec2(1, 1) => -45,
            _ => tf.rotation,
        };*/

        Camera.target = tf.position.as2d().round();
    }

    public void draw()
    {
        //Tilemap.pushSprite(spr, tf);
        Platform.drawText("¡Thé quìçk brõwñ fôx jümped ovÉr the låzý dog!", vec2i(), vec2i(1, 0));
    }
}