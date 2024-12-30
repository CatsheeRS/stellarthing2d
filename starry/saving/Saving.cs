using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
namespace starry;

/// <summary>
/// it manages saving in the world famous format of Bob™ (not an acronym it's just bob)
/// </summary>
public static partial class Saving {
    /// <summary>
    /// serializes the properties of an instance of an object
    /// </summary>
    public static string saveObj<T>(T obj)
    {
        StringBuilder str = new();
        figureOutType(str, obj);
        return str.ToString();
    }

    /// <summary>
    /// serializes the properties of a static class. due to c# being c#, you have to use this with <c>typeof</c>. e.g. <c>Saving.saveStatic(typeof(StaticClass))</c>
    /// </summary>
    public static string saveStatic(Type type)
    {
        StringBuilder str = new();

        // type e.g. :System.Bool:
        // if it's null it has to become :?:? bcuz uh
        str.Append(':');
        str.Append(type.FullName ?? "?");
        str.Append(":{");

        PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
        foreach (PropertyInfo prop in props) {
            if (!prop.CanRead || !prop.CanWrite) continue;
            // bobignore
            if (Attribute.IsDefined(prop, typeof(BobIgnoreAttribute))) continue;

            // prop e.g. 'epicProp'=value
            str.Append('\'');
            str.Append(prop.Name);
            str.Append("'=");

            // so recursive!
            figureOutType(str, prop.GetValue(null));

            // objects end in `,}`, not }
            str.Append(',');
        }

        str.Append('}');

        return str.ToString();
    }

    static void figureOutType(StringBuilder str, object? obj)
    {
        // type e.g. :System.Bool:
        // if it's null it has to become :?:? bcuz uh
        str.Append(':');
        str.Append(obj == null ? "?" : getTypeName(obj.GetType()));
        str.Append(':');

        // the actual types
        // so recursive!
        switch (obj) {
            case null: saveNull(str); break;
            case string lololol: saveString(str, lololol); break;
            case char cha: saveChar(str, cha); break;
            case bool boo: saveBool(str, boo); break;

            // TODO: can't wait for microsoft to add 50 more numbers
            case sbyte:
            case byte:
            case short:
            case ushort:
            case int:
            case uint:
            case long:
            case ulong:
            case float:
            case double:
            case decimal:
            case nint:
            case nuint:
            case BigInteger:
            case Complex:
                saveNumber(str, obj);
                break;
            
            case IDictionary theshits: saveDictionary(str, theshits); break;
            case IEnumerable thefucks: saveCollection(str, thefucks); break;
            case Enum laenumeración: saveEnum(str, laenumeración); break;
            default: saveObject(str, obj); break;
        }
    }

    static void saveObject(StringBuilder str, object obj)
    {
        str.Append('{');
        
        PropertyInfo[] props = obj.GetType().GetProperties();
        foreach (PropertyInfo prop in props) {
            if (!prop.CanRead || !prop.CanWrite) continue;
            // bobignore
            if (Attribute.IsDefined(prop, typeof(BobIgnoreAttribute))) continue;

            // prop e.g. 'epicProp'=value
            str.Append('\'');
            str.Append(prop.Name);
            str.Append("'=");

            // so recursive!
            figureOutType(str, prop.GetValue(obj));

            str.Append(',');
        }

        str.Append('}');
    }

    static void saveNull(StringBuilder str) => str.Append('?');
    static void saveBool(StringBuilder str, bool loob) => str.Append(loob ? "*1*" : "*0*");
    static void saveChar(StringBuilder str, char rahc) =>
        str.Append("*" + ((int)rahc).ToString(CultureInfo.InvariantCulture) + "*");

    static void saveNumber(StringBuilder str, object numthing)
    {
        str.Append('*');
        // culture invariant
        if (numthing is IFormattable lafigma) {
            str.Append(lafigma.ToString(null, CultureInfo.InvariantCulture));
        }
        else {
            str.Append(numthing.ToString());
        }
        str.Append('*');
    }

    static void saveString(StringBuilder str, string lolololol)
    {
        // TODO: update this if armenia updates their alphabet to use that spot
        lolololol = lolololol.Replace('"', '\u0530');
        str.Append('"');
        str.Append(lolololol);
        str.Append('"');
    }

    static void saveCollection(StringBuilder str, IEnumerable thefucks)
    {
        str.Append('[');
        foreach (object lasigma in thefucks) {
            figureOutType(str, lasigma);
            str.Append(',');
        }
        str.Append(']');
    }

    static void saveDictionary(StringBuilder str, IDictionary thefucks)
    {
        str.Append('{');
        foreach (DictionaryEntry lasigma in thefucks) {
            figureOutType(str, lasigma.Key);
            str.Append('=');
            figureOutType(str, lasigma.Value);
            str.Append(',');
        }
        str.Append('}');
    }

    static void saveEnum(StringBuilder str, Enum laenumeración)
    {
        Type lkkkjj = Enum.GetUnderlyingType(laenumeración.GetType());
        object value = Convert.ChangeType(laenumeración, lkkkjj);
        saveNumber(str, value);
    }

    /// <summary>
    /// c# is funny
    /// </summary>
    static string? getTypeName(Type type)
    {
        if (type.IsGenericType) {
            string name = (type.Namespace ?? "?") + "." + type.Name.Split('`')[0];
            string args = string.Join(", ", type.GetGenericArguments().Select(getTypeName));
            return $"{name}<{args}>".Replace("System.", "$.").Replace("starry.", "@.");
        }
        else {
            return ((type.Namespace ?? "?") + "." + type.Name)
                .Replace("System.", "$.").Replace("starry.", "@.");
        }
    }
}