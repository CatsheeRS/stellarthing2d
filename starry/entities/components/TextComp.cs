using System;
using System.Numerics;
using Raylib_cs;
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
    public required Font font { get; set; }
    /// <summary>
    /// the font size in pixels
    /// </summary>
    public required uint fontSize { get; set; }
    /// <summary>
    /// the position (based in global coordinates)
    /// </summary>
    public vec2 position { get; set; } = vec2();
    /// <summary>
    /// the size the font is limited to, does nothing is <c>wordWrap</c> is disabled
    /// </summary>
    public vec2 size { get; set; } = vec2();
    /// <summary>
    /// if true, the text will wrap around <c>size</c>
    /// </summary>
    public bool wordWrap { get; set; } = false;
    /// <summary>
    /// the color of the text
    /// </summary>
    public color color { get; set; } = color.white;

    /// <summary>
    /// run in your update function
    /// </summary>
    public void update(string text)
    {
        int spacing = 5;
        if (!wordWrap) {
            Raylib.DrawTextPro(font.rlfont, text, new Vector2((float)position.x, (float)position.y), Vector2.Zero,
                0, fontSize, spacing, new Color(color.r, color.g, color.b, color.a));
        }
        // help
        // this is just stolen from the raylib examples
        else {
            int length = text.Length;
            vec2 textOffset = vec2();
            float scale = fontSize / (float)font.rlfont.BaseSize;

            WordWrapState state = wordWrap ? WordWrapState.measureState : WordWrapState.drawState;

            int startLine = -1;
            int endLine = -1;
            int lastChar = -1;

            int i = 0;
            int k = 0;
            foreach (char cha in text) {
                int codepointByteCount = 0;
                int codepoint = Raylib.GetCodepoint(text[i].ToString(), ref codepointByteCount);
                int index = Raylib.GetGlyphIndex(font.rlfont, codepoint);

                if (codepoint == 0x3f) codepointByteCount = 1;
                i += codepointByteCount - 1;
                
                float glyphWidth = 0;
                if (codepoint != '\n') {
                    unsafe {
                        glyphWidth = (font.rlfont.Glyphs[index].AdvanceX == 0) ? font.rlfont.Recs[index].Width *
                            scale : font.rlfont.Glyphs[index].AdvanceX * scale;
                    }
                    if (i + 1 < length) glyphWidth += spacing;
                }

                // NOTE: When wordWrap is ON we first measure how much of the text we can draw before going outside
                // of the rec container
                // We store this info in startLine and endLine, then we change states, draw the text between those two
                // variables and change states again and again recursively until the end of the text (or until we get
                // outside of the container).
                // When wordWrap is OFF we don't need the measure state so we go to the drawing state immediately
                // and begin drawing on the next line before we can get outside the container.
                if (state == WordWrapState.measureState) {
                    if (char.IsWhiteSpace((char)codepoint)) endLine = i;

                    if ((textOffset.x + glyphWidth) > size.x) {
                        endLine = (endLine < 1) ? i : endLine;
                        if (i == endLine) endLine -= codepointByteCount;
                        if ((startLine + codepointByteCount) == endLine) endLine = i - codepointByteCount;
                        state = state == WordWrapState.measureState ? WordWrapState.drawState :
                            WordWrapState.measureState;
                    }
                    else if ((i + 1) == length) {
                        endLine = i;
                        state = state == WordWrapState.measureState ? WordWrapState.drawState :
                            WordWrapState.measureState;
                    }
                    else if (codepoint == '\n') state = state == WordWrapState.measureState ? WordWrapState.drawState :
                                                                 WordWrapState.measureState;
                    
                    if (state == WordWrapState.drawState) {
                        textOffset.x = 0;
                        i = startLine;
                        glyphWidth = 0;

                        // save character position when we switch states
                        int tmp = lastChar;
                        lastChar = k - 1;
                        k = tmp;
                    }
                }
                else {
                    if (codepoint == '\n') {
                        if (!wordWrap) {
                            textOffset.y += (font.rlfont.BaseSize + font.rlfont.BaseSize / 2) * scale;
                            textOffset.x = 0;
                        }
                    }
                    else {
                        if (!wordWrap && (textOffset.x + glyphWidth > size.x)) {
                            textOffset.y += (font.rlfont.BaseSize + font.rlfont.BaseSize / 2) * scale;
                            textOffset.x = 0;
                        }

                        // when text overflows rectangle height limit, just stop drawing
                        if ((textOffset.y + font.rlfont.BaseSize * scale) > size.y) break;

                        // // Draw selection background
                        // bool isGlyphSelected = false;
                        // if ((selectStart >= 0) && (k >= selectStart) && (k < (selectStart + selectLength)))
                        // {
                        //     DrawRectangleRec((Rectangle){ rec.x + textOffsetX - 1, rec.y + textOffsetY, glyphWidth, (float)font.baseSize*scaleFactor }, selectBackTint);
                        //     isGlyphSelected = true;
                        // }

                        // finally draw the shit
                        if (!char.IsWhiteSpace((char)codepoint)) {
                            Raylib.DrawTextCodepoint(font.rlfont, codepoint, new Vector2((float)(size.x +
                                textOffset.x), (float)(size.y + textOffset.y)), fontSize, new Color(color.r, color.g,
                                color.b, color.a));
                        }
                    }

                    if (wordWrap && (i == endLine)) {
                        textOffset.y += (font.rlfont.BaseSize + font.rlfont.BaseSize / 2) * scale;
                        textOffset.x = 0;
                        startLine = endLine;
                        endLine = -1;
                        glyphWidth = 0;
                        k = lastChar;
                        //selectStart += lastChar - k;
                        state = state == WordWrapState.measureState ? WordWrapState.drawState :
                            WordWrapState.measureState;
                    }
                }

                // avoid leading spaces
                if (textOffset.x != 0 || codepoint != ' ') textOffset.x += glyphWidth;

                i++;
                k++;
            }
        }
    }

    enum WordWrapState
    {
        measureState,
        drawState
    }
}