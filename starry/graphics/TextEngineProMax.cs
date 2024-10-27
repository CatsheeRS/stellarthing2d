using System;
using static starry.Starry;

namespace starry;

/// <summary>
/// sprite font manager thing for text stuff since text is
/// </summary>
public static partial class TextEngineProMax {
    /// <summary>
    /// draws a single character from the sprite font (you should probably use <c>drawText()</c>)
    /// </summary>
    public static void drawCharacter(char c, vec2i pos, string fontpath, out vec2i charSize, color color)
    {
        var cra = getCharFile(c, fontpath);
        charSize = cra.size;
        Platform.drawTexture(cra, pos, cra.size, color);
    }

    /// <summary>
    /// draws many characters :D
    /// </summary>
    public static void drawText(string s, string fontpath, vec2i pos, vec2i spacing, color color)
    {
        int xoffset = spacing.x;
        int yoffset = 0;
        vec2i fontsize = getCharFile('\0', fontpath).size;

        foreach (var c in s) {
            if (c == '\n') {
                yoffset += fontsize.y + spacing.y;
                xoffset = spacing.x;
                continue;
            }

            if (c == ' ') {
                xoffset += fontsize.x + spacing.x;
                continue;
            }

            drawCharacter(c, pos + vec2i(xoffset, yoffset), fontpath, out vec2i lol, color);
            xoffset += lol.x + spacing.x;
        }
    }

    /// <summary>
    /// we're too cool for ttfs or spritesheets. characters are supposed to be inside the folder specified in fontpath, just copy the filenames from the default font.
    /// </summary>
    public static Sprite getCharFile(char c, string fontpath)
    {
        string crappath = fontpath + "/" + c switch {
            '0' => "0",
            '1' => "1",
            '2' => "2",
            '3' => "3",
            '4' => "4",
            '5' => "5",
            '6' => "6",
            '7' => "7",
            '8' => "8",
            '9' => "9",
            'á' => "aal",
            'Á' => "aau",
            'â' => "acl",
            'Â' => "acu",
            '´' => "acute",
            'å' => "adl",
            'Å' => "adu",
            'à' => "agl",
            'À' => "agu",
            'a' => "al",
            '&' => "ampersand",
            '\'' => "apostrophe",
            '@' => "at",
            'ã' => "atl",
            'Ã' => "atu",
            'A' => "au",
            'ä' => "aul",
            'Ä' => "auu",
            '\\' => "backslash",
            'b' => "bl",
            '[' => "bracket_left",
            ']' => "bracket_right",
            'B' => "bu",
            '˚' => "circle_accent_thing",
            '^' => "circumflex",
            'c' => "cl",
            ':' => "colon",
            ',' => "comma",
            '©' => "copyright",
            'ç' => "crl",
            'Ç' => "cru",
            'C' => "cu",
            '{' => "curly_left",
            '}' => "curly_right",
            '÷' => "divide",
            'd' => "dl",
            'ð' => "dll",
            'Ð' => "dlu",
            '$' => "dollar",
            'D' => "du",
            'é' => "eal",
            'É' => "eau",
            'ê' => "ecl",
            'Ê' => "ecu",
            'è' => "egl",
            'È' => "egu",
            'e' => "el",
            '=' => "equal",
            'E' => "eu",
            'ë' => "eul",
            'Ë' => "euu",
            '!' => "exclamation",
            'f' => "fl",
            'F' => "fu",
            '.' => "full_period",
            'g' => "gl",
            '`' => "grave",
            '>' => "greater",
            'G' => "gu",
            '#' => "hash",
            'h' => "hl",
            'H' => "hu",
            'í' => "ial",
            'Í' => "iau",
            'î' => "icl",
            'Î' => "icu",
            'ì' => "igl",
            'Ì' => "igu",
            'i' => "il",
            'I' => "iu",
            'ï' => "iul",
            'Ï' => "iuu",
            'j' => "jl",
            'J' => "ju",
            'k' => "kl",
            'K' => "ku",
            '<' => "less",
            'l' => "ll",
            'L' => "lu",
            'm' => "ml",
            'M' => "mu",
            '×' => "multiply",
            'n' => "nl",
            'ñ' => "ntl",
            'Ñ' => "ntu",
            'N' => "nu",
            'ó' => "oal",
            'Ó' => "oau",
            'ô' => "ocl",
            'Ô' => "ocu",
            'ò' => "ogl",
            'Ò' => "ogu",
            'o' => "ol",
            'ø' => "oll",
            'Ø' => "olu",
            'õ' => "otl",
            'Õ' => "otu",
            'O' => "ou",
            'ö' => "oul",
            'Ö' => "ouu",
            '(' => "parenthesis_left",
            ')' => "parenthesis_right",
            '%' => "percent",
            '|' => "pipe", // #| (2001)
            'p' => "pl",
            'þ' => "pll",
            'Þ' => "plu",
            '±' => "plus_minus",
            '+' => "plus",
            '-' => "possible_hyphen",
            '¯' => "possible_macron", // edit is in fact a macron https://en.wikipedia.org/wiki/Latin-1_Supplement
            'P' => "pu",
            'q' => "ql",
            'Q' => "qu",
            '?' => "question",
            '"' => "quote",
            '®' => "registered",
            'r' => "rl",
            'R' => "ru",
            ';' => "semicolon",
            's' => "sl",
            '/' => "slash",
            'ª' => "small_a",
            'º' => "small_o",
            '¡' => "spanish_exclamation",
            '¿' => "spanish_question",
            'S' => "su",
            '¸' => "tail_accent_thing", // cedilla you idiot, the polish ą is a different accent 
            '~' => "tilde",
            't' => "tl",
            'T' => "tu",
            'ú' => "ual",
            'Ú' => "uau",
            'û' => "ucl",
            'Û' => "ucu",
            'ù' => "ugl",
            'Ù' => "ugu",
            'u' => "ul",
            '¨' => "umlaut",
            '_' => "underscore",
            'U' => "uu",
            'ü' => "uul",
            'Ü' => "uuu",
            'v' => "vl",
            'V' => "vu",
            'w' => "wl",
            'W' => "wu",
            'x' => "xl",
            'X' => "xu",
            'ý' => "yal",
            'Ý' => "yau",
            'y' => "yl",
            'Y' => "yu",
            'ÿ' => "yul",
            'z' => "zl",
            'Z' => "zu",
            _ => "unknown"
        } + ".png";
        return load<Sprite>(crappath);
    }
}