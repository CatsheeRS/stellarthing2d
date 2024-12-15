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
    readonly int speed = 1;

    public async void create()
    {
        tile = new(await load<Sprite>("species/bobdown1.png")) {
            position = (0, 0, 0),
        };
    }

    public async void update(double delta)
    {
        vec2i dir = (0, 0);
        // it's adding so you can move diagonally
        if (Input.isKeymapHeld("move_left")) dir += (-1, 0);
        if (Input.isKeymapHeld("move_right")) dir += (1, 0);
        if (Input.isKeymapHeld("move_up")) dir += (0, -1);
        if (Input.isKeymapHeld("move_down")) dir += (0, 1);

        // actually move
        tile!.position += (dir * (speed, speed) * (vec2)(delta, delta)).as3d(tile.position.z);

        // animation stuff
        // my shitty pixel art is too shitty to make a left/right animation
        // TODO actually fucking animate
        if (dir.y < -0.5) tile.sprite = await load<Sprite>("species/bobup1.png");
        else tile.sprite = await load<Sprite>("species/bobdown1.png");

        // the famous camera
        Tilemap.camPosition = tile.position.as2d();
    }

    public void draw() => Tilemap.pushTile(tile!);
}