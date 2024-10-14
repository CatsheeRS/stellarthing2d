using System;
using System.Collections.Generic;
using static starry.Starry;

namespace starry;

/// <summary>
/// adds text stuff :D
/// TODO: add word wrap and stuff
/// </summary>
public class TextComp {
    /// <summary>
    /// the font to use
    /// </summary>
    public Font font { get; set; } = load<Font>(settings.defaultFont);
    /// <summary>
    /// the font size in pixels
    /// </summary>
    public uint fontSize { get; set; } = 30;
    /// <summary>
    /// the position (based in global coordinates)
    /// </summary>
    public vec2i position { get; set; } = vec2i();
    /// <summary>
    /// rotation in degrees
    /// </summary>
    public double rotation { get; set; } = 0;
    /// <summary>
    /// the size the font is limited to, does nothing is <c>wordWrap</c> is disabled
    /// </summary>
    public vec2i size { get; set; } = vec2i();
    /// <summary>
    /// if true, the text will wrap around <c>size</c>
    /// </summary>
    public bool wordWrap { get; set; } = false;
    /// <summary>
    /// the color of the text
    /// </summary>
    public color color { get; set; } = color.white;
    //Viewport view = new(settings.renderSize);

    /// <summary>
    /// run in your update function
    /// </summary>
    public void draw(string text)
    {
        // if (!wordWrap) {
            //TextUtil.drawText(text, position, fontSize, rotation, color, font, view);
        // }

        // this is chatgpt i can't be bothered to write this
        // else {
        //     string[] words = text.Split(' ');
        //     Queue<string> wrappedLines = [];
        //     string currentLine = "";
        //     uint lineLength = (uint)(size.x / fontSize);
            
        //     // i sure hope this doesn't annihilate performance
        //     // TODO: don't annihilate performance
        //     foreach (var word in words) {
        //         if ((currentLine.Length + word.Length + 1) > lineLength) {
        //             wrappedLines.Enqueue(currentLine.Trim());
        //             currentLine = word + " ";
        //         }
        //         else currentLine += word + " ";
        //     }
            
        //     if (!string.IsNullOrEmpty(currentLine.Trim())) {
        //         wrappedLines.Enqueue(currentLine.Trim());
        //     }

        //     // render stuff (i actually wrote this)
        //     // it's in scissor mode since it looks better and is easier than stopping when we run out of lines
        //     Raylib.BeginScissorMode(position.x, position.y, size.x, size.y);
        //         int yoffset = 0;
        //         while (wrappedLines.Count > 0) {
        //             TextUtil.drawText(wrappedLines.Dequeue(), position, fontSize, rotation, color, font, rt);
        //             yoffset += (int)(fontSize / 100 * settings.fontCharacterSize.y);
        //         }
        //     Raylib.EndScissorMode();
        // }
    }
}