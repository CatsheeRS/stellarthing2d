using System;
using System.IO;
namespace starry;

/// <summary>
/// manages all things saving and serialization
/// </summary>
public static class Save {
    /// <summary>
    /// the current save
    /// </summary>
    public static string universe { get; set; } = "";
    /// <summary>
    /// the folder where saves are going to
    /// </summary>
    public static string savedir { get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), universe); }

    
}