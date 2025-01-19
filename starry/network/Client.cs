using System;
using SimpleTCP;

namespace starry;

/// <summary>
/// client :)
/// </summary>
public class Client {
    public static Client thisClient = new();

    public Client()
    {
        thisClient = this;

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
    /// <summary>
    /// called when the server sends data to the client
    /// </summary>
    public OnDataReceived? onDataReceived { get; set; }
    internal SimpleTcpClient? tcpClient;

    public delegate void OnDataReceived(string data, string type);
}
