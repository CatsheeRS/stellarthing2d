using System;
using Raylib_cs;
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

        Raylib.DrawTexturePro(
            font.texture,
            new Rectangle(
                settings.fontCharacterSize.x * settings.fontCharacters[c].x,
                settings.fontCharacterSize.y * settings.fontCharacters[c].y,
                settings.fontCharacterSize.x,
                settings.fontCharacterSize.y
            ),
            new Rectangle(
                pos.x, pos.y,
                settings.fontCharacterSize.x * (fontSize / 100),
                settings.fontCharacterSize.y * (fontSize / 100)
            ),
            new System.Numerics.Vector2(0, 0),
            0, new Color(tint.r, tint.g, tint.b, tint.a)
        );
    }

    /// <summary>
    /// draws many characters :D
    /// </summary>
    public static void drawText(string s, vec2i pos, uint fontSize, double rot, color tint, Font font, RenderTexture2D rt)
    {
        uint xoffset = 0;
        uint yoffset = 0;
        vec2i size = vec2i();

        // draw to a texture so we can also rotate it lol
        Raylib.BeginTextureMode(rt);
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
        Raylib.EndTextureMode();

        // actually render and rotate
        Raylib.DrawTexturePro(
            rt.Texture,
            new Rectangle(0, 0, size.x, size.y),
            new Rectangle(pos.x, pos.y, size.x, size.y),
            new System.Numerics.Vector2(size.x / 2, size.y / 2),
            (float)rot,
            new Color(tint.r, tint.g, tint.b, tint.a)
        );
        log(size, pos, size / vec2i(2, 2), rot, tint);
    }
}