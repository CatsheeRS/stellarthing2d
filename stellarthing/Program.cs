using System.IO;
using System.Linq;
using System.Threading.Tasks;
using starry;
using static starry.Starry;
namespace stellarthing;

internal class Program {
    internal static async Task Main(string[] args)
    {
        await create(new StarrySettings {
            startup = () => {},
            verbose = isDebug() || args.Contains("--verbose") || args.Contains("-v"),
            gameName = "Stellarthing",
            gameVersion = (0, 10, 0),
            fullscreen = true,
            assetPath = Path.GetFullPath("assets"),
        });
    }
}