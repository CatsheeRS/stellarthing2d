using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace starry;

/// <summary>
/// this is what clients use to talk to the server
/// </summary>
public static class Client
{
    /// <summary>
    /// info for the current client
    /// </summary>
    public static ClientInfo thisClient { get; internal set; } = new();
    /// <summary>
    /// invoked when the client receives data from server
    /// </summary>
    public static event OnDataReceived? onDataReceived;
    static TcpClient? client;
    static bool running = false;

    /// <summary>
    /// connects to an ip or address and the provided port. returns true if it succeeded, returns false otherwise
    /// </summary>
    public static async Task<bool> connect(string address, int port)
    {
        try {
            running = true;
            client = new TcpClient(address, port);
            Starry.log($"Client {thisClient.username} ({thisClient.id}) connected to {address}:{port}");

            await handleConnection(client.GetStream());

            return true;
        }
        catch (Exception e) {
            Starry.log($"Couldn't connect: {e.Message}");
            return false;
        }
    }

    static async Task handleConnection(NetworkStream stream)
    {
        while (true) {
            byte[] reply = new byte[Server.BUFFER_SIZE];
            int bytesRead = await stream.ReadAsync(reply, 0, reply.Length);
            
            string msg = Encoding.UTF8.GetString(reply, 0, bytesRead);
            var parsed = Server.deserializeData(msg);
            onDataReceived?.Invoke(parsed.Item1, parsed.Item2, parsed.Item3);
        }
    }

    /// <summary>
    /// uploads something from the client to the server. the type is used for the server to deserialize it.
    /// </summary>
    public static async Task upload(string type, object obj)
    {
        if (client == null) return;

        string msg = Server.serializeData(thisClient.id, type, obj);
        byte[] shiaLeBuffer = Encoding.UTF8.GetBytes(msg);
        await client.GetStream().WriteAsync(shiaLeBuffer, 0, shiaLeBuffer.Length);
    }

    /// <summary>
    /// disconnects from the server :)
    /// </summary>
    public static void disconnect()
    {
        running = false;
        client?.Close();
    }

    public delegate void OnDataReceived(string senderId, string type, string obj);
}

/// <summary>
/// client info :)
/// </summary>
public struct ClientInfo {
    public ClientInfo()
    {
        Client.thisClient = this;

        // placeholder username
        if (username == "") username = $"player_{id}";
    }

    /// <summary>
    /// its the username :)
    /// </summary>
    public string username { get; set; } = "";
    /// <summary>
    /// random 4 characters long base64 string (enough for 16.7 million players)
    /// </summary>
    public string id { get; } = StMath.randomBase64(4);
    /// <summary>
    /// if true the client can do things like kicking people and stuff
    /// </summary>
    public bool isAdmin { get; internal set; } = false;

    internal NetworkStream? stream;
}
