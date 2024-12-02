using System.IO;
using System.Threading.Tasks;
using starry;
using static starry.Starry;
namespace stellarthing;

internal class Program {
    internal async static Task Main(string[] args)
    {
        await create(new StarrySettings {
            startup = async () => {
                Graphics.defaultFont = await Assets.load<Font>("fonts/pixel-unicode.ttf");
            },
            gameName = "Stellarthing",
            gameVersion = (0, 10, 0),
            assetPath = Path.GetFullPath("assets"),
        });
    }
}