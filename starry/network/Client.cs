using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WatsonTcp;

namespace starry;

public class Client
{
    public static ClientInfo currClient;

    public static event OnDataReceived? onDataReceived;

    private static WatsonTcpClient? tcpClient;

    public static bool connected;
    private static TaskCompletionSource<bool> connectionTcs;

    public static async Task<bool> connect(string address, int port)
    {
        connectionTcs = new TaskCompletionSource<bool>();

        try
        {
            tcpClient = new(address, port);
            tcpClient.Events.MessageReceived += messageRecieved;
            tcpClient.Settings.Logger += (severity, s) =>
            {
                Console.WriteLine(s);
            };
            
            tcpClient.Connect();
            connected = true;
            
            await sendMessage("starry.CLIENT_CONNECTED", currClient);
        
            Starry.log($"Client {currClient.username} ({currClient.id}) connected to {address}:{port}");
            connectionTcs.TrySetResult(true);
        }
        catch (Exception e)
        {
            Starry.log($"server connection error: {e}");
            connectionTcs.TrySetResult(false);
        }

        return await connectionTcs.Task;
    }
    
    private static void messageRecieved(object? sender, MessageReceivedEventArgs args)
    {
        var deserializedData = Server.deserializeData(Encoding.UTF8.GetString(args.Data));
        ClientInfo cli = JsonConvert.DeserializeObject<ClientInfo>(deserializedData.Item1);
        
        onDataReceived?.Invoke(cli, deserializedData.Item2, deserializedData.Item3);
    }

    public static async Task sendMessage(string type, object? obj)
    {
        if (!connected) return;
        
        var serializeData = Server.serializeData(currClient, type, obj);
        
        Console.WriteLine($"Sending data {serializeData}");
        tcpClient?.SendAsync(Encoding.UTF8.GetBytes(serializeData));
    }
    
    public delegate void OnDataReceived(ClientInfo sender, string type, string obj);
}