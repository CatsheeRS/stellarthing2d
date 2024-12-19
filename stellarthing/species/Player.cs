using System.Threading.Tasks;
using starry;
using static starry.Starry;
namespace stellarthing;

/// <summary>
/// player
/// </summary>
public class Player : IEntity {
    public EntityType getEntityType() => EntityType.gameWorld;
    public string getName() => "Player";
    public string[] getInitGroups() =>
        [Groups.PLAYER_GROUP, Groups.HUMAN_GROUP, Groups.SPECIES_GROUP];
    
    TileComp? tile;
    TileComp? lol;
    AnimationSprite? walkDown;
    AnimationSprite? walkUp;
    readonly double speed = 3.5;

    public async void create()
    {
        walkDown = new(0.25,
            await load<Sprite>("species/bobdown1.png"),
            await load<Sprite>("species/bobdown2.png"),
            await load<Sprite>("species/bobdown3.png"),
            await load<Sprite>("species/bobdown4.png")
        );
        walkUp = new(0.25,
            await load<Sprite>("species/bobup1.png"),
            await load<Sprite>("species/bobup2.png"),
            await load<Sprite>("species/bobup3.png"),
            await load<Sprite>("species/bobup4.png")
        );

        // my shitty pixel art is too shitty to make a left/right animation
        tile = new(walkDown, walkDown, walkUp, walkDown) {
            position = (0, 0, 0),
        };
        lol = new(await load<Sprite>("tiles/testl.png"),
            await load<Sprite>("tiles/testr.png"),
            await load<Sprite>("tiles/testt.png"),
            await load<Sprite>("tiles/testb.png")) {
            position = (1, 2, 0),
        };
        Timer timer = new(1, true);
        timer.timeout += () => {
            log("lmao");
        };
        timer.start();
    }

    public void update(double delta)
    {
        vec2i dir = (0, 0);
        // it's adding so you can move diagonally
        if (Input.isKeymapHeld("move_left")) dir += (-1, 0);
        if (Input.isKeymapHeld("move_right")) dir += (1, 0);
        if (Input.isKeymapHeld("move_up")) dir += (0, -1);
        if (Input.isKeymapHeld("move_down")) dir += (0, 1);

        // actually move
        tile!.position += (dir * (vec2)(speed, speed) * (vec2)(delta, delta)).as3d(tile.position.z);

        // animation stuff
        // it shouldn't go back to looking down when you didn't press anything
        if (dir > (0, 0)) {
            tile.side = dir switch {
                (1, 0) => TileSide.right,
                (-1, 0) => TileSide.left,
                (0, 1) => TileSide.bottom,
                (0, -1) => TileSide.top,
                _ => tile.side
            };

            if (!walkDown!.playing) walkDown.start();
            if (!walkUp!.playing) walkUp.start();
        }
        else {
            walkDown!.stop();
            walkUp!.stop();
        }

        // the famous camera
        Tilemap.camPosition = tile.position.as2d();
    }

    public void draw()
    {
        Tilemap.pushTile(lol!);
        Tilemap.pushTile(tile!);
    }
}