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
            startup = () => {
                Entities.addEntity(new Player());
            },
            verbose = isDebug() || args.Contains("--verbose") || args.Contains("-v"),
            gameName = "Stellarthing",
            gameVersion = (0, 10, 0),
            fullscreen = true,
            assetPath = Path.GetFullPath("assets"),
            renderSize = (320, 180),
            antiAliasing = false,
            tileSize = (16, 16),
            keymap = new() {
                {"move_left", [Key.a, Key.left]},
                {"move_right", [Key.d, Key.right]},
                {"move_up", [Key.w, Key.up]},
                {"move_down", [Key.s, Key.down]},
            }
        });
    }
}