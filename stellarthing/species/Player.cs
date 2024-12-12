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
    int fucker = 0;

    public async void create()
    {
        tile = new(await load<TileSprite>("tiles/test.png")) {
            position = (0, 0, 0),
        };
    }

    public void update(double delta)
    {
        tile!.position -= (0.005, 0, 0);
        
        // we dont have timers yet
        fucker++;
        if (fucker % 10 == 0) {
            tile.side = tile.side.rotateClockwise();
            fucker = 0;
        }

        //log("is key held ", Input.isKeyHeld(Key.space), ", is key just pressed ", Input.isKeyJustPressed(Key.space), ", is key released ", Input.isKeyJustReleased(Key.space));
    }

    public void draw() => Tilemap.pushTile(tile!);
}