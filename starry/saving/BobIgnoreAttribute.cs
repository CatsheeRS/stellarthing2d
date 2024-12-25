using System;
namespace starry;

/// <summary>
/// ignores the property when serializing stuff
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class BobIgnoreAttribute : Attribute {}