using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Reflection;
namespace starry;

/// <summary>
/// it manages saving in the world famous format of Bob™ (not an acronym it's just bob)
/// </summary>
public static partial class Saving {
    /// <summary>
    /// deserializes the properties of an instance of an object
    /// </summary>
    public static T load<T>(string data)
    {
        LoaderState l = new() {
            input = data
        };

        // recursive :)
        object? thingy = parse(l);

        // then turn the shit into actual data stuff
        #pragma warning disable CS8600 // mate
        #pragma warning disable CS8603 // mate
        return (T)deserialize(thingy, typeof(T));
        #pragma warning restore CS8603 // mate
        #pragma warning restore CS8600 // mate
    }

    static object? parse(LoaderState l)
    {
        // first get type
        string type;
        if (match(l, ":")) {
            type = readWhile(l, c => c != ':');
            l.i++;
        }

        // parse shit
        if (match(l, "{")) return parseObject(l);
        else if (match(l, "[")) return parseArray(l);
        else if (match(l, "*")) return parseNumber(l);
        else if (match(l, "\"")) return parseString(l);
        else if (match(l, "?")) return null;
        else throw new Exception($"mate your Bob™ is busted mate: unexpected character {l.input[l.i]}");
    }

    static object? parseObject(LoaderState l)
    {
        Dictionary<object, object?> data = new();

        // empty object
        if (match(l, "}")) {
            l.i++;
            return data;
        }

        while (true) {
            // technically anything can be a dictionary key!
            // parseString() handles identifiers (between single quotes)
            object? key = parse(l);

            // =
            l.i++;

            // anything can be a dictionary value too
            object? value = parse(l);
            data.Add(key ?? new object(), value);

            if (match(l, "}")) break;
            else l.i++; // ,
        }

        return data;
    }

    static object? parseArray(LoaderState l)
    {
        List<object?> data = new();

        // empty collection
        if (match(l, "[")) {
            l.i++;
            return data;
        }

        while (true) {
            // technically anything can be a dictionary key!
            // parseString() handles identifiers (between single quotes)
            object? val = parse(l);
            data.Add(val);

            if (match(l, "]")) break;
            else l.i++; // ,
        }

        return data;
    }

    static object? parseNumber(LoaderState l)
    {
        string str = readWhile(l, c => c != '"');
        // remove trailing *
        str = str[..^1];

        // parse the damn thing
        // TODO: could potentially get funky
        if (str.Contains('.')) return Complex.Parse(str, CultureInfo.InvariantCulture);
        else return BigInteger.Parse(str, CultureInfo.InvariantCulture);
    }

    static object? parseString(LoaderState l)
    {
        string str = readWhile(l, c => c != '"');
        // remove trailing "
        str = str[..^1];

        // unescape quotes
        // TODO: update this if armenia updates their alphabet to use that spot
        str = str.Replace('\u0530', '"');
        return str;
    }

    static bool match(LoaderState l, string text)
    {
        // that funny .. thing is just making a substring
        if (l.input[l.i..].StartsWith(text)) {
            l.i += text.Length;
            return true;
        }
        return false;
    }

    static string readWhile(LoaderState l, Func<char, bool> condition)
    {
        int start = l.i;
        while (l.i < l.input.Length && condition(l.input[l.i])) {
            l.i++;
        }
        // that funny .. thing is just making a substring
        string s = l.input[start..l.i];
        return s;
    }

    static object? deserialize(object? thingy, Type type)
    {
        if (thingy == null) return null;

        // nullables :)
        if (Nullable.GetUnderlyingType(type) is Type underlying) {
            deserialize(thingy, underlying);
        }

        // famous primitives
        // TODO make this less fucking hideous
        // TODO c# 79 support for algebraic data types, macro templates, and quantum record classes
        if (type.IsPrimitive || type == typeof(string) || type == typeof(BigInteger) ||
        type == typeof(Complex) || type.IsEnum) {
            return Convert.ChangeType(thingy, type);
        }

        // arrays
        if (type.IsArray) {
            Type elementType = type.GetElementType()!;
            var list = (List<object>)thingy;
            Array array = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < list.Count; i++) {
                array.SetValue(deserialize(list[i], elementType), i);
            }
            return array;
        }

        // dictionaries (needs to go before collections because dictionaries are, in fact,
        // collections too)
        if (typeof(IDictionary).IsAssignableFrom(type) && type.IsGenericType) {
            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];
            var dict = (Dictionary<object, object>)thingy;
            var ionary = (IDictionary)Activator.CreateInstance(type)!;

            foreach (var kvp in dict) {
                var key = Convert.ChangeType(kvp.Key, keyType);
                var value = deserialize(kvp.Value, valueType);
                ionary.Add(key, value);
            }
            return ionary;
        }

        // collections
        // my brain is working overtime i need something to ease my mind
        if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType) {
            Type elementType = type.GetGenericArguments()[0];
            var list = (List<object>)thingy;
            var collection = (IList)Activator.CreateInstance(type)!;
            foreach (var item in list) {
                collection.Add(deserialize(item, elementType));
            }
            return collection;
        }

        // other shits
        var obj = Activator.CreateInstance(type);
        var mate = (Dictionary<string, object>)thingy;

        foreach (var kvp in mate) {
            var property = type.GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
            if (property != null && property.CanWrite) {
                var value = deserialize(kvp.Value, property.PropertyType);
                property.SetValue(obj, value);
            }

            var field = type.GetField(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
            if (field != null) {
                var value = deserialize(kvp.Value, field.FieldType);
                field.SetValue(obj, value);
            }
        }

        return obj;
    }
}

/// <summary>
/// seriously
/// </summary>
class LoaderState {
    public int i = 0;
    public string input = "";
}
