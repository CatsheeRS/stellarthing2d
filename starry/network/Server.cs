using System.Collections.Concurrent;
using System.Text;
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
    /// <summary>
    /// how many times per second the server updates. it's 20 because minecraft uses 20
    /// </summary>
    public const int TICK_RATE = 20;
    public static event UpdateLoop? onUpdate;
    public static event PlayerConnected? onPlayerConnected;
    public static event PlayerDisconnected? onPlayerDisconnected;
    public static event DataReceived? onDataReceived;
    public static uint playersConnected { get; private set; } = 0;

    static SimpleTcpServer? server;
    static Timer serverTick = new(1 / TICK_RATE, true);

    /// <summary>
    /// used by the server to run functions that require authentication
    /// </summary>
    static Client dummyClient = new() {
        username = "STELLARTHING_SERVER",
        isAdmin = true,
    };

    static ConcurrentDictionary<string, Client> clients = new();

    /// <summary>
    /// starts an epic awesome cool handsome server. the host will become an admin but won't be connected automatically. returns true if it succeeded
    /// </summary>
    public static bool create(uint maxPlayers, Client? host)
    {
        server = new SimpleTcpServer();
        server.Start(GAME_PORT);

        onPlayerConnected += (client) => {
            playersConnected += 1;
            Starry.log($"Client {client.username} ({client.id}) connected!");

            // we can't have 5 gazillion players in the same server
            if (playersConnected > maxPlayers) {
                kickPlayer(dummyClient, client.id);
                Starry.log($"Client {client.username} ({client.id}) can't connect, server is full");
                return;
            }

            clients.TryAdd(client.id, client);
        };

        onPlayerDisconnected += (client) => {
            playersConnected -= 1;
            clients.TryRemove(client.id, out _);
            Starry.log($"Client {client.username} ({client.id}) disconnected!");
        };

        server.DataReceived += (mate, msg) => {
            string msgmsg = Encoding.UTF8.GetString(msg.Data);
            string[] data = msgmsg.Split('=', 2);
            onDataReceived?.Invoke(data[0], data[1]);
        };

        serverTick.start();
        serverTick.timeout += () => onUpdate?.Invoke(Window.deltaTime);

        if (host != null) host.isAdmin = true;
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

        // mate
        client.tcpClient.DataReceived += (a, msg) => {
            string msgmsg = Encoding.UTF8.GetString(msg.Data);
            string[] data = msgmsg.Split('=', 2);
            client.gmsjgjsjgjrjsjgjjisrjigjjisrj(data[0], data[1]);
        };

        Starry.log($"Connected {client.username} ({client.id}) to {ip}:{port}");
        return true;
    }

    /// <summary>
    /// disconnects from a server :)
    /// </summary>
    public static void disconnect(Client client)
    {
        client.tcpClient?.Disconnect();
        Starry.log($"Disconnected {client.username} ({client.id})");
    }

    /// <summary>
    /// stops the server. the client is for authentication. returns true if it succeeded.
    /// </summary>
    public static bool cleanup(Client client)
    {
        if (!client.isAdmin) {
            Starry.log("Can't stop server; must be an admin to stop the server.");
            return false;
        }

        serverTick.stop();
        server?.Stop();
        Starry.log("Stopped server.");
        return true;
    }

    /// <summary>
    /// sends a object to a client. the id is a 4 character long base64 string (16.7 million possibilities). whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    public static void sendToPlayer(string id, object obj, string type)
    {
        // TODO: don't.
        // this will probably be horrible when you have more than a couple players
        server?.BroadcastLine(serializeData(
            new SpecificClientMessage() {
                clientId = id,
                data = obj,
            },
            type
        ));
    }

    /// <summary>
    /// sends an object to every client. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    /// </summary>
    public static void sendToAll(object obj, string type)
    {
        server?.BroadcastLine(serializeData(obj, type));
    }

    /// <summary>
    /// kicks a player from its ID. the kicker has to be an admin for this to work. returns true if it succeeded.
    /// </summary>
    public static bool kickPlayer(Client kicker, string kicked)
    {
        Client kickedd = clients[kicked];
        if (!kicker.isAdmin) {
            Starry.log($"Can't kick {kickedd.username} ({kickedd.id}), {kicker.username} {kicker.id} must be an admin.");
            return false;
        }

        kickedd.tcpClient?.Disconnect();
        Starry.log($"Player {kicker.username} ({kicker.id}) kicked {kickedd.username} ({kickedd.id})");
        return true;
    }

    /// <summary>
    /// uploads something to the server. whatever you send is gonna be serialized in json. the type is used by the server to deserialize it back
    /// </summary>
    public static void upload(Client sender, object obj, string type)
    {
        sender.tcpClient?.WriteLine(serializeData(obj, type));
    }

    /// <summary>
    /// uploads something to the server and waits for a reply. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back. returns a type followed by the message in json.
    /// </summary>
    public static async Task<(string, string)> ask(Client sender, object obj, string type)
    {
        return await Task.Run(() => {
            if (sender.tcpClient == null) return ("", "");

            Message msg = sender.tcpClient.WriteAndGetReply(serializeData(obj, type));
            
            string[] sigming = msg.MessageString.Split('=');
            return (sigming[0], sigming[1]);
        });
    }

    internal static string serializeData(object obj, string type) => 
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