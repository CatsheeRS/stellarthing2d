using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Silk.NET.GLFW;
using WatsonTcp;

namespace starry;

public class Server
{
    public const int GAME_PORT = 41885;
    public const int TICK_RATE = 20;
    public const int BUFFER_SIZE = 1024;
    
    private static Timer serverTick = new(1 / TICK_RATE, true);
    
    public static event UpdateLoop? onUpdate;
    public static event PlayerConnected? onPlayerConnected;
    public static event PlayerDisconnected? onPlayerDisconnected;
    public static event DataReceived? onDataReceived;

    private static WatsonTcpServer server;
    
    private static ConcurrentDictionary<string, ClientInfo> clients = new();
    private static ConcurrentDictionary<string, Guid> idIPs = new();
    
    public static ClientInfo serverInfo = new ClientInfo
    {
        username = "Server",
        id = "bLeH",
        isAdmin = true
    };
    
    public static async Task create()
    {
        string ip = "127.0.0.1";
        if (!Environment.GetCommandLineArgs().Contains("--server-debug"))
        {
            //ip = "0.0.0.0";
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                    networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    var properties = networkInterface.GetIPProperties();
                    foreach (var address in properties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ip = address.Address.ToString();
                        }
                    }
                }
            }
        }

        server = new WatsonTcpServer(ip, GAME_PORT);
        
        server.Events.MessageReceived += messageRecieved;
        server.Events.ClientDisconnected += clientDisconnect;
        serverTick.start();
        serverTick.timeout += () => onUpdate?.Invoke(Window.deltaTime);
        server.Settings.DebugMessages = true;
        server.Settings.Logger += (severity, s) =>
        {
            Console.WriteLine(s);
        };
        server.Start();
        
        Starry.log($"Server listening on {ip}:{GAME_PORT}");
        Starry.log(server.IsListening);
    }
    
    private static void messageRecieved(object? sender, MessageReceivedEventArgs args)
    {
        Console.WriteLine("BAD");
        var data = args.Data;
        var deserializedData = deserializeData(Encoding.UTF8.GetString(data));
        
        ClientInfo clientSender = JsonConvert.DeserializeObject<ClientInfo>(deserializedData.Item1);
        if (deserializedData.Item2 == "starry.CLIENT_CONNECTED")
        { 
            clients.TryAdd(clientSender.id, clientSender);
            idIPs.TryAdd(clientSender.id, args.Client.Guid);
            
            Console.WriteLine($"Player {clientSender.username} ({clientSender.id}) joined the server at {args.Client.IpPort}");
        }
        
        onDataReceived?.Invoke(clientSender, deserializedData.Item2, deserializedData.Item3);
    }

    public static List<ClientInfo> getPlayers()
    {
        List<ClientInfo> players = new List<ClientInfo>();
        foreach (var client in clients)
        {
            Console.WriteLine(client.Value.id + ": " + client.Value.username);
            players.Add(client.Value);
        }
        
        return players;
    }
    
    private static void clientDisconnect(object? sender, DisconnectionEventArgs args)
    {
        idIPs.TryRemove(args.Client.IpPort, out Guid id);
        clients.TryRemove(args.Client.IpPort, out ClientInfo client);
    }

    public static async Task sendToPlayer(ClientInfo client, string type, object? obj, ClientInfo ass)
    {
        byte[] reply = Encoding.UTF8.GetBytes(serializeData(ass, type, obj));
        Console.WriteLine($"sending to {idIPs[client.id]}");
        await server.SendAsync(idIPs[client.id], reply);
    }
    
    public static async Task sendToPlayer(ClientInfo client, string type, object? obj)
    {
        sendToPlayer(client, type, obj, client);
    }
    
    public static async Task sendToAll(string type, object? obj)
    {
        foreach (var client in clients) {
            sendToPlayer(client.Value, type, obj, serverInfo);
        }
    }
    
    public static async Task sendToAll(ClientInfo sender, string type, object? obj)
    {
        foreach (var client in clients) {
            sendToPlayer(client.Value, type, obj, sender);
        }
    }
    
    /// <summary>
    /// internal utility function. the type is so it knows what to deserialize as
    /// </summary>
    public static string serializeData(ClientInfo clientId, string type, object? obj)
    {
        return $"{JsonConvert.SerializeObject(clientId)}&{type}&{JsonConvert.SerializeObject(obj)}";
    }

    public static string arrayifyArray(string s)
    {
        s = s.Replace("\\", "");
        if (s.StartsWith("\""))
            s = s.Substring(1);
                    
        if (s.EndsWith("\""))
            s = s.Remove(s.Length-1);
        
        return s;
        
        s = s.Replace("\\", "");
        if (s.StartsWith("\""))
            s = s.Substring(1);
                    
        if (s.EndsWith("\""))
            s = s.Remove(s.Length-1);

        return s;
    }

    /// <summary>
    /// internal utility function. the type is so it knows what to deserialize as. returns the sender ID followed by the type followed by the object
    /// </summary>
    public static (string, string, string) deserializeData(string src)
    {
        string[] lo = src.Split('&', 3);
        return (lo[0], lo[1], lo[2]);
    }
    
    public delegate void UpdateLoop(double delta);
    public delegate void PlayerConnected(string id, string username);
    public delegate void PlayerDisconnected(string id, string username);
    public delegate void DataReceived(ClientInfo sender, string type, string obj);
}