using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using starry;
using static starry.Starry;
namespace stellarthing;

/// <summary>
/// player
/// </summary>
public class Player : IEntity {
    public override EntityType entityType => EntityType.GAME_WORLD;
    public override string name => "Player";
    public override string[] initGroups =>
        [Groups.PLAYER_GROUP, Groups.HUMAN_GROUP, Groups.SPECIES_GROUP];
    
    Tile? tile;
    Tile? lol;
    AnimationSprite? walkDown;
    AnimationSprite? walkUp;
    AnimationSprite? walkRight;
    AnimationSprite? walkLeft;
    TileParticles? lasparticulas;

    readonly double speed = 3.5;

    public bool localPlayer = true;
    public ClientInfo networkOwner;

    private Font ft;

    private Sync<vec3> pos = new((0,0,0));
    private static Dictionary<ClientInfo, Player> players = new();
    public override async void create()
    {
        if (!localPlayer)
        {
            log("new enetity");
            pos.networkOwner = networkOwner;
            pos.data = new vec3(0, 0, 0);
        }

        walkDown = new AnimationSprite(0.25,
            await load<Sprite>("species/bobdown1.png"),
            await load<Sprite>("species/bobdown2.png"),
            await load<Sprite>("species/bobdown3.png"),
            await load<Sprite>("species/bobdown4.png")
        )!;
        walkUp = new AnimationSprite(0.25,
            await load<Sprite>("species/bobup1.png"),
            await load<Sprite>("species/bobup2.png"),
            await load<Sprite>("species/bobup3.png"),
            await load<Sprite>("species/bobup4.png")
        );
        walkRight = new AnimationSprite(0.25,
            await load<Sprite>("species/bobright1.png"),
            await load<Sprite>("species/bobright2.png"),
            await load<Sprite>("species/bobright3.png"),
            await load<Sprite>("species/bobright4.png")
        );
        walkLeft = new AnimationSprite(0.25,
            await load<Sprite>("species/bobleft1.png"),
            await load<Sprite>("species/bobleft2.png"),
            await load<Sprite>("species/bobleft3.png"),
            await load<Sprite>("species/bobleft4.png")
        );


        if (localPlayer)
        {
            ft = await load<Font>("font/pixel-unicode.ttf");
        }
        
        tile = Entities.addComponent<Tile>(ent2ref(this));
        tile.sprite = new TileSprite(walkLeft, walkRight, walkUp, walkDown);
        
        lol = new() {
            sprite = new(await load<Sprite>("tiles/testl.png"),
                         await load<Sprite>("tiles/testr.png"),
                         await load<Sprite>("tiles/testt.png"),
                         await load<Sprite>("tiles/testb.png")),
            position = (1, 2, 0),
        };

        lasparticulas = new() {
            particle = await load<Sprite>("white.png"),
            amountFunc = () => (uint)StMath.randint(200, 6000),
            durationFunc = () => StMath.randfloat(1, 5),
            positionStartFunc = () => tile.position,
            positionEndFunc = () => StMath.randvec2((-10, -10), (10, 10)).as3d(tile.position.z),
            rotationStartFunc = () => 0,
            rotationEndFunc = () => StMath.randfloat(-360, 360),
            colorStartFunc = () => color.white,
            colorEndFunc = () => (255, 255, 255, 0),
        };

        if (!localPlayer) return;
        
        var aaa = await load<Audio>("music/Legacy Menu.mp3");
        aaa.play();
        
        if (settings.server)
        {
            await Server.create();

            Server.onDataReceived += (sender, type, s) =>
            {
                log("recieved data");
                if (type == "augh")
                {
                    Server.sendToAll("augh", "");
                }
                
                if (type == "DO YOU LIKE BEANS ?????")
                {
                    log("YES I DO LIKE BEANS");
                    Server.sendToPlayer(sender, "YES I DO LIKE BEANS", "h");
                }
                
                if (type == "PlayerCreate")
                {
                    string? vars = JsonConvert.SerializeObject(Server.getPlayers());
                    Console.WriteLine($"Sending PlayerCreate for client {sender.username} ({sender.id}): {s}");
                    
                    Server.sendToAll("PlayerCreate", vars);
                }
                
                if (type == "Player_starry.vec3_Update")
                {
                    s = Server.arrayifyArray(s);
                    Console.WriteLine($"Sending PlayerUpdate for client {sender.username} ({sender.id}): {s}");
                    Server.sendToAll(sender,"Player_starry.vec3_Update", s);
                }
            };
        }
        else
        {
            string username = Environment.GetCommandLineArgs()
                .FirstOrDefault(arg => arg.StartsWith("--username="))?
                .Substring("--username=".Length) ?? "Mr Peepeepoopoo";
            
            Client.currClient = new ClientInfo(username);
            Client.currClient.id = StMath.randomBase64(4);
            
            string ip = Environment.GetCommandLineArgs()
                .FirstOrDefault(arg => arg.StartsWith("--ip="))?
                .Substring("--ip=".Length) ?? "127.0.0.1";

            if (Environment.GetCommandLineArgs().Contains("--server-debug"))
                ip = "127.0.0.1";
            
            Client.connect(ip, Server.GAME_PORT);
            Client.onDataReceived += (sender, type, s) =>
            {  
                log(type);
                if (type == "augh")
                {
                    log("recieved augh");
                    lasparticulas!.emit();
                    return;
                }
                
                if (type == "PlayerCreate")
                {
                    log("creating players");
                    s = Server.arrayifyArray(s);
                    log(s);
                    
                    var playerList = JsonConvert.DeserializeObject<List<ClientInfo>>(s);
                    log("finished deseiralize");
                    log(playerList);

                    foreach (ClientInfo currPl in playerList)
                    {
                        if (players.ContainsKey(currPl) || currPl.id == Client.currClient.id)
                            continue;

                        Player playerNPC = new Player();
                        playerNPC.localPlayer = false;
                        playerNPC.networkOwner = currPl;
                        
                        log($"Player initialized: {name}, networkOwner: {(networkOwner.id != null ? networkOwner.username : "null")}");
                        players.Add(currPl, playerNPC);
                        Entities.addEntity(playerNPC);   
                    }
                }
            };
            
            await Client.sendMessage("PlayerCreate", "h");
            players.Add(Client.currClient, this);
            networkOwner = Client.currClient;
            
            pos.networkOwner = networkOwner;
        }
    }

    public override async void update(double delta)
    {
        vec2i dir = (0, 0);

        if (localPlayer)
        {
            // it's adding so you can move diagonally
            if (Input.isKeymapHeld("move_left")) dir += (-1, 0);
            if (Input.isKeymapHeld("move_right")) dir += (1, 0);
            if (Input.isKeymapHeld("move_up")) dir += (0, -1);
            if (Input.isKeymapHeld("move_down")) dir += (0, 1);   
            
            // actually move

            if (dir != (0, 0))
            {
                vec3 orig = pos.data + (dir * (vec2)(speed, speed) * (vec2)(delta, delta)).as3d(tile.position.z);
                pos.data = (orig.x, orig.y, orig.z);
            }
        }
        
        if (pos.data != tile!.position)
        {
            tile!.position = pos.data;
        }

        // animation stuff
        // it shouldn't go back to looking down when you didn't press anything
        if (dir != (0, 0)) {
            tile.side = dir switch {
                (1, 0) => TileSide.RIGHT,
                (-1, 0) => TileSide.LEFT,
                (0, 1) => TileSide.BOTTOM,
                (0, -1) => TileSide.TOP,
                _ => tile.side
            };

            // haha
            tile.sprite!.bottom = walkDown!;
            if (!walkDown!.playing) walkDown.start();
            if (!walkUp!.playing) walkUp.start();
            if (!walkLeft!.playing) walkLeft.start();
            if (!walkRight!.playing) walkRight.start();
        }
        else {
            // haha
            tile.sprite!.bottom = await load<Sprite>("species/bobdown0.png");
            walkDown!.stop();
            walkUp!.stop();
            walkLeft!.stop();
            walkRight!.stop();
            walkDown.currentFrame = 0;
            walkUp.currentFrame = 0;
            walkLeft.currentFrame = 0;
            walkRight.currentFrame = 0;
        }

        if (settings.server || !localPlayer) return;
        
        // the famous camera
        Tilemap.camPosition = tile!.position.as2d();

        // why though
        if (Input.isKeyJustPressed(Key.SPACE))
        {
            if (Client.connected)
                Client.sendMessage("augh", "h");
            else
                lasparticulas!.emit();
        }
        
        if (Input.isKeyJustPressed(Key.F9))
        {
            Client.sendMessage("DO YOU LIKE BEANS ?????", "h");
        }

        if (Input.isKeyJustPressed(Key.E) && doooooone)
        {
            doooooone = true;
            Player newPlayer = new Player();
            newPlayer.localPlayer = false;
            newPlayer.networkOwner = new ClientInfo
            {
                id = StMath.randomBase64(10),
                isAdmin = true,
                username = StMath.randomBase64(10),
            };
            
            players.Add(newPlayer.networkOwner, newPlayer);
            Entities.addEntity(newPlayer);
        }
    }

    public override void draw()
    {
        Tilemap.pushTile(lol!);
        if (localPlayer)
        {
            string txt = "Connected players: ";

            foreach (Player plyr in players.Values)
            {
                txt += plyr.networkOwner.username;
                txt += ", ";

                vec2 initialPos = plyr.tile.globalPosition;
                initialPos.y -= 10;
                
                Graphics.drawText(plyr.networkOwner.username, ft, initialPos, color.white);   
            }
            
            txt = txt.Remove(txt.Length-1);
            Graphics.drawText(txt, ft, (0, 0), color.white);   
        }
        
        lasparticulas!.draw();
    }

    private bool doooooone; 
}