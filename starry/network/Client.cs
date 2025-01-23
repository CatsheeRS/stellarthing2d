using System;
using System.Net.Sockets;
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
