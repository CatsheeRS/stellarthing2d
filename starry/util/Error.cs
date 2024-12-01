namespace starry;

/// <summary>
/// use with Result<T>. the type is supposed to be a constant, it's a string so there's less conflicts
/// </summary>
public struct Error(string msg, string type) {
    /// <summary>
    /// the message :D
    /// </summary>
    public string message { get; set; } = msg;
    /// <summary>
    /// the error type, should be a constant, it's a string so there's less conflicts. example: ASSET_NOT_FOUND = "STARRY_ASSET_NOT_FOUND"
    /// </summary>
    public string type { get; set; } = type;
}
