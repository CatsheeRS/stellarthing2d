using System.Net.Sockets;
namespace starry;

/// <summary>
/// client info :)
/// </summary>
public struct ClientInfo {
    public ClientInfo(string username)
    {
        this.username = username;
        id = StMath.randomBase64(4);
        Client.thisClient = this;
    }

    public ClientInfo()
    {
        username = $"player_{id}";
        id = StMath.randomBase64(4);
        Client.thisClient = this;
    }

    /// <summary>
    /// its the username :)
    /// </summary>
    public string username { get; set; } = "";
    /// <summary>
    /// random 4 characters long base64 string (enough for 16.7 million players)
    /// </summary>
    public string id { get; }
    /// <summary>
    /// if true the client can do things like kicking people and stuff
    /// </summary>
    public bool isAdmin { get; internal set; } = false;

    internal NetworkStream? stream;
}
