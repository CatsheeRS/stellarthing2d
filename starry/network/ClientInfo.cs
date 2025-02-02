using System;

namespace starry;

/// <summary>
/// client info :)
/// </summary>
public struct ClientInfo {
    public ClientInfo(string username)
    {
        Console.WriteLine("makking new client info");
        this.username = username;
    }

    public ClientInfo()
    {
        Console.WriteLine("makking new client info");
        username = $"player_{id}";
    }

    /// <summary>
    /// its the username :)
    /// </summary>
    public string username { get; set; } = "";
    /// <summary>
    /// random 4 characters long base64 string (enough for 16.7 million players)
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// if true the client can do things like kicking people and stuff
    /// </summary>
    public bool isAdmin { get; set; } = false;
}