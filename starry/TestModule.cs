using System;
using System.Threading.Tasks;
namespace starry;

public static class TestModule {
    // shut up
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public static async Task create()
    {
        Console.WriteLine("hola");
    }

    public static async Task update()
    {
        Console.WriteLine("hola hola");
    }

    public static async Task cleanup()
    {
        Console.WriteLine("adios");
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}