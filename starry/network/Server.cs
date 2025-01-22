using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace starry;

/// <summary>
/// server :)
/// </summary>
public static class Server {
    /// <summary>
    /// port i randomly chose to use specifically for stellarthing. if you're using this game engine for your own game for some reason, you should probably choose another port
    /// </summary>
    public const int GAME_PORT = 41885;
    /// <summary>
    /// how many times per second the server updates. it's 20 because minecraft uses 20
    /// </summary>
    public const int TICK_RATE = 20;
    public static event UpdateLoop? onUpdate;
    public static event PlayerConnected? onPlayerConnected;
    public static event PlayerDisconnected? onPlayerDisconnected;
    public static event DataReceived? onDataReceived;
    public static uint playersConnected { get; private set; } = 0;

    static Timer serverTick = new(1 / TICK_RATE, true);

    /// <summary>
    /// used by the server to run functions that require authentication
    /// </summary>
    internal static Client dummyClient = new() {
        username = "STELLARTHING_SERVER",
        isAdmin = true,
    };

    /// <summary>
    /// starts an epic awesome cool handsome server. the host will become an admin but won't be connected automatically. returns true if it succeeded
    /// </summary>
    public static bool create(uint maxPlayers, Client? host)
    {
    }

    /// <summary>
    /// connects to an existing server. returns true if it succeeded
    /// </summary>
    public static bool connect(string ip, int port, Client client)
    {
        
    }

    /// <summary>
    /// disconnects from a server :)
    /// </summary>
    public static void disconnect(Client client)
    {
        
    }

    /// <summary>
    /// stops the server. the client is for authentication. returns true if it succeeded.
    /// </summary>
    public static bool cleanup(Client client)
    {

    }

    /// <summary>
    /// sends a object to a client. the id is a 4 character long base64 string (16.7 million possibilities). whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    public static void sendToPlayer(string id, object obj, string type)
    {
        
    }

    /// <summary>
    /// sends an object to every client. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    /// </summary>
    public static void sendToAll(object obj, string type)
    {
        
    }

    /// <summary>
    /// kicks a player from its ID. the kicker has to be an admin for this to work. returns true if it succeeded.
    /// </summary>
    public static bool kickPlayer(Client kicker, string kicked)
    {
        
    }

    /// <summary>
    /// uploads something to the server. whatever you send is gonna be serialized in json. the type is used by the server to deserialize it back
    /// </summary>
    public static void upload(Client sender, object obj, string type)
    {
        
    }

    /// <summary>
    /// uploads something to the server and waits for a reply. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back. returns a type followed by the message in json.
    /// </summary>
    public static async Task<(string, string)> ask(Client sender, object obj, string type)
    {
        
    }

    /// <summary>
    /// it serializes data. this is just used by the networking API for communicating what type the json they're sending even is.
    /// </summary>
    public static string serializeData(object obj, string type) => 
        $"{type}={JsonConvert.SerializeObject(obj, new JsonSerializerSettings {
            // shut up marge shut up
            //ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
        })}";

    // delegates
    public delegate void UpdateLoop(double delta);
    public delegate void PlayerConnected(Client client);
    public delegate void PlayerDisconnected(Client client);
    public delegate void DataReceived(string obj, string type);
}