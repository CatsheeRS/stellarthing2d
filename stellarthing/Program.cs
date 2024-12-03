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
                Graphics.defaultFont = await Assets.load<Font>("font/pixel-unicode.fnt");
            },
            gameName = "Stellarthing",
            gameVersion = (0, 10, 0),
            fullscreen = true,
            frameRate = 144,
            assetPath = Path.GetFullPath("assets"),
        });
    }
}