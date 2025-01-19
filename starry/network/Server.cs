using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTCP;

namespace starry;

/// <summary>
/// server :)
/// </summary>
public static class Server {
    /// <summary>
    /// port i randomly chose to use specifically for stellarthing. if you're using this game engine for your own game for some reason, you should probably choose another port
    /// </summary>
    public const int GAME_PORT = 41885;
    public static UpdateLoop? onUpdate;
    public static PlayerConnected? onPlayerConnected;
    public static PlayerDisconnected? onPlayerDisconnected;
    public static DataReceived? onDataReceived;
    public static uint playersConnected { get; private set; } = 0;

    static SimpleTcpServer? server;

    /// <summary>
    /// used by the server to run functions that require authentication
    /// </summary>
    static Client dummyClient = new() {
        username = "STELLARTHING_SERVER",
        isAdmin = true,
    };

    static ConcurrentHashSet<Client> clients = [];

    /// <summary>
    /// starts an epic awesome cool handsome server. returns true if it succeeded
    /// </summary>
    public static bool create(uint maxPlayers)
    {
        server = new SimpleTcpServer();
        server.Start(GAME_PORT);

        onPlayerConnected += (client) => {
            playersConnected += 1;
            Starry.log($"Client {client.username} ({client.id}) connected!");

            // we can't have 5 gazillion players in the same server
            if (playersConnected > maxPlayers) {
                kickPlayer(dummyClient, client);
                Starry.log($"Client {client.username} ({client.id}) can't connect, server is full");
            }
        };

        onPlayerDisconnected += (client) => {
            playersConnected -= 1;
            Starry.log($"Client {client.username} ({client.id}) disconnected!");
        };

        server.DataReceived += (mate, msg) => {
            string[] data = msg.MessageString.Split('=');
            onDataReceived?.Invoke(data[0], data[1]);
        };

        Starry.log("Server created!");
        return true;
    }

    /// <summary>
    /// connects to an existing server. returns true if it succeeded
    /// </summary>
    public static bool connect(string ip, int port, Client client)
    {
        client.tcpClient = new SimpleTcpClient();
        client.tcpClient.Connect(ip, port);
        upload(client, client, "starry.CLIENT_CONNECTED");
        Starry.log($"Connected {client.username} ({client.id}) to {ip}:{port}");
        return true;
    }

    /// <summary>
    /// stops the server. the client is for authentication
    /// </summary>
    public static bool cleanup(Client client)
    {
        
    }

    /// <summary>
    /// sends a object to a client. the id is a 4 character long base64 string (16.7 million possibilities). whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    public static void sendToPlayer(string id, object obj, string type) {}

    /// <summary>
    /// sends an object to every client. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    /// </summary>
    public static void sendToAll(object obj, string type) {}

    /// <summary>
    /// kicks a player. the kicker has to be an admin for this to work
    /// </summary>
    public static void kickPlayer(Client kicker, Client kicked) {}

    /// <summary>
    /// uploads something to the server. whatever you send is gonna be serialized in json. the type is used by the server to deserialize it back
    /// </summary>
    public static void upload(Client sender, object obj, string type)
    {
        sender.tcpClient?.Write($"{type}={JsonConvert.SerializeObject(obj)}");
    }

    /// <summary>
    /// uploads something to the server and waits for a reply. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back. returns a type followed by the message in json.
    /// </summary>
    public static async Task<(string, string)> ask(Client sender, object obj, string type)
    {
        return await Task.Run(() => {
            if (sender.tcpClient == null) return ("", "");

            Message msg = sender.tcpClient.WriteAndGetReply(
                $"{type}={JsonConvert.SerializeObject(obj)}");
            
            string[] sigming = msg.MessageString.Split('=');
            return (sigming[0], sigming[1]);
        });
    }

    /// <summary>
    /// called by the engine
    /// </summary>
    internal static void update()
    {
        onUpdate?.Invoke(Window.deltaTime)
    }

    // delegates
    public delegate void UpdateLoop(double delta);
    public delegate void PlayerConnected(Client client);
    public delegate void PlayerDisconnected(Client client);
    public delegate void DataReceived(string type, string obj);
}