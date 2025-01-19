namespace starry;

/// <summary>
/// this is broadcasted to every client when it's supposed to only be sent to one client because simpleTCP doesn't have a function to send something to a specific client lmao
/// </summary>
public struct SpecificClientMessage {
    public string clientId { get; set; }
    public object data { get; set; }
}