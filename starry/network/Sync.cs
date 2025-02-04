using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace starry;

public class Sync<T>
{
    private T _data;
    private string owner;

    public ClientInfo? networkOwner;
    public T data
    {
        get
        {
            return _data;
        }
        set
        {
            if (networkOwner == null) return;
            if (!Client.connected)
            {
                _data = value;
                return;
            }
            
            if (networkOwner?.id == Client.currClient.id)
            {
                Console.WriteLine($"[SYNC] Set request from {networkOwner?.username}");
                
                Client.sendMessage($"{owner}_{_data.GetType()}_Update", value);
            }
        }
    }

    public Sync(T? data)
    {
        _data = data;
        owner = new StackTrace().GetFrame(1)?.GetMethod()?.DeclaringType?.Name ?? "Invalid";
        Client.onDataReceived += (sender, type, dataObj) =>
        {
            if (type != $"{owner}_{_data.GetType()}_Update" || sender.id != networkOwner?.id) return;
                        
            Console.WriteLine("-SYNC-----------");
            dataObj = Server.arrayifyArray(dataObj);
            Console.WriteLine($"[SYNC] Pre arrayification is {dataObj}");
            var deserialized = JsonConvert.DeserializeObject<T>(dataObj, new JsonSerializerSettings
            {
                Error = delegate(object sender, ErrorEventArgs args)
                {
                    Console.WriteLine(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                },
            });

            Console.WriteLine($"[SYNC] Pre deserialization is {dataObj}");
            Console.WriteLine($"[SYNC] Updating {sender.username} with pre update: {_data}");
            _data = deserialized;
            Console.WriteLine($"[SYNC] Updating {sender.username} with post update: {_data}");
        };
    }
}