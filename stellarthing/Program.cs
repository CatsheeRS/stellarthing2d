using System.Threading.Tasks;
using starry;
namespace stellarthing;

internal class Program {
    internal async static Task Main(string[] args)
    {
        await Starry.create(new StarrySettings {
            startup = () => {},
            gameName = "Stellarthing",
            gameVersion = (0, 10, 0),
        });
    }
}