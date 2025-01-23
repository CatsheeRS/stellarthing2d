using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
    /// <summary>
    /// size of the buffer used in messages and stuff, in bytes.
    /// </summary>
    public const int BUFFER_SIZE = 1024;
    public static event UpdateLoop? onUpdate;
    public static event PlayerConnected? onPlayerConnected;
    public static event PlayerDisconnected? onPlayerDisconnected;
    public static event DataReceived? onDataReceived;
    public static uint playersConnected { get; private set; } = 0;

    static Timer serverTick = new(1 / TICK_RATE, true);
    static bool running = false;
    static TcpListener? server;
    static ConcurrentDictionary<string, ClientInfo> clients = new();
    static ConcurrentDictionary<string, bool> admins = new();

    /// <summary>
    /// used by the server to run functions that require authentication
    /// </summary>
    internal static ClientInfo dummyClient = new() {
        username = "STELLARTHING_SERVER",
        isAdmin = true,
    };

    /// <summary>
    /// starts an epic awesome cool handsome server. the host will become an admin but won't be connected automatically.
    /// </summary>
    public static async Task create(ClientInfo? host)
    {
        running = true;
        server = new TcpListener(IPAddress.Any, GAME_PORT);
        server.Start();

        // make the fuckinmg host a fucking admin
        if (host != null) {
            admins[host.Value.id] = true;
        }

        Starry.log($"Server has started! Listening on port {GAME_PORT}");

        // loop for receiving stuff :)
        while (running) {
            TcpClient client = await server.AcceptTcpClientAsync();
            await handleClient(client);
        }
    }

    static async Task handleClient(TcpClient client)
    {
        NetworkStream streamma = client.GetStream();

        try {
            while (true) {
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytesRead = await streamma.ReadAsync(buffer, 0, buffer.Length);

                // client disconnected
                if (bytesRead == 0) break;

                string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // TODO remove this because the whole point of a server is receiving messages
                Starry.log("Message received ", msg);

                var parsed = deserializeData(msg);
                onDataReceived?.Invoke(parsed.Item1, parsed.Item2, parsed.Item3);
            }
        }
        catch (Exception e) {
            Starry.log("Client connection error: " + e.Message);
        }

        client.Close();
    }

    /// <summary>
    /// stops the server
    /// </summary>
    public static void cleanup()
    {
        // this is gonna stop the loop thing
        running = false;
        server?.Stop();
    }

    /// <summary>
    /// sends a object to a client. the id is a 4 character long base64 string (16.7 million possibilities). whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    public static async Task sendToPlayer(string id, string type, object obj)
    {
        var locliente = clients[id];
        if (locliente.stream == null) {
            Starry.log($"Invalid stream! (ID {id})");
            return;
        }

        byte[] reply = Encoding.UTF8.GetBytes(serializeData(id, type, obj));
        await locliente.stream.WriteAsync(reply, 0, reply.Length);
    }

    /// <summary>
    /// sends an object to every client. whatever you send is gonna be serialized in json. the type is used for clients to deserialize it back
    /// </summary>
    public static async Task sendToAll(string type, object obj)
    {
        foreach (var clients in clients) {
            await sendToPlayer(clients.Value.id, type, obj);
        }
    }

    /// <summary>
    /// internal utility function. the type is so it knows what to deserialize as
    /// </summary>
    public static string serializeData(string clientId, string type, object obj)
    {
        return $"{clientId}&{type}&{JsonConvert.SerializeObject(obj, new JsonSerializerSettings() {
            // references are cool :)
            PreserveReferencesHandling = PreserveReferencesHandling.All,
        })}";
    }

    /// <summary>
    /// internal utility function. the type is so it knows what to deserialize as. returns the sender ID followed by the type followed by the object
    /// </summary>
    public static (string, string, string) deserializeData(string src)
    {
        string[] lo = src.Split('&', 3);
        return (lo[0], lo[1], lo[2]);
    }

    /// <summary>
    /// does a bunch of checks to see if the client is suspicious amongst us. returns true if it succeeded, returns false otherwise
    /// </summary>
    public static bool validateClient(string id, bool logErrors = true)
    {
        // TODO: actually check shitfuck
        ClientInfo client = clients[id];
        return true;
    }

    // delegates
    public delegate void UpdateLoop(double delta);
    public delegate void PlayerConnected(string id, string username);
    public delegate void PlayerDisconnected(string id, string username);
    public delegate void DataReceived(string senderId, string type, string obj);
}