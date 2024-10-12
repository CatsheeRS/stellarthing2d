using System;
using static starry.Starry;

namespace starry;

/// <summary>
/// sprite font manager thing for text stuff since text is
/// </summary>
public static partial class TextUtil {
    /// <summary>
    /// draws a single character from the sprite font (you should probably use <c>drawText()</c>)
    /// </summary>
    public static void drawCharacter(char c, vec2i pos, uint fontSize, color tint, Font font)
    {
        // if the character doesn't exist it becomes a square
        if (!settings.fontCharacters.ContainsKey(c)) c = '\0';

        #pragma warning disable CS8604 // Possible null reference argument.
        Platform.renderTexture(
            font.texture,
            settings.fontCharacterSize * settings.fontCharacters[c],
            settings.fontCharacterSize,
            pos, settings.fontCharacterSize * vec2i((int)(fontSize / 100), (int)(fontSize / 100)),
            0, vec2i(), tint
        );
        #pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// draws many characters :D
    /// </summary>
    public static void drawText(string s, vec2i pos, uint fontSize, double rot, color tint, Font font, Viewport view)
    {
        uint xoffset = 0;
        uint yoffset = 0;
        vec2i size = vec2i();

        // draw to a texture so we can also rotate it lol
        view.start(color.transparent);
            foreach (var c in s) {
                if (c == '\n') {
                    yoffset += (uint)(settings.fontCharacterSize.y * (fontSize / 100));
                    size += vec2i(0, (int)yoffset);
                    xoffset = 0;
                    continue;
                }

                drawCharacter(c, pos + vec2i(0, (int)yoffset), fontSize, tint, font);
                xoffset += (uint)(settings.fontCharacterSize.x * (fontSize / 100));
                size += vec2i((int)xoffset, 0);
            }
        view.end();

        // actually render and rotate
        view.render(vec2i(), size, pos, size, 0, size / vec2i(2, 2), tint);
        //log(size, pos, size / vec2i(2, 2), rot, tint);
    }
}