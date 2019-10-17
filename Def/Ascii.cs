using System;
using System.Collections.Generic;
using System.Text;

namespace Def
{
    public static class Ascii
    {
        /// <summary>
        /// Control chars
        /// </summary>
        public static readonly char[] AllChars = new[]
        {
            (char)0, (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9,
            (char)10, (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19,
            (char)20, (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29,
            (char)30, (char)31, (char)32, (char)33, (char)34, (char)35, (char)36, (char)37, (char)38, (char)39,
            (char)40, (char)41, (char)42, (char)43, (char)44, (char)45, (char)46, (char)47, (char)48, (char)49,
            (char)50, (char)51, (char)52, (char)53, (char)54, (char)55, (char)56, (char)57, (char)58, (char)59,
            (char)60, (char)61, (char)62, (char)63, (char)64, (char)65, (char)66, (char)67, (char)68, (char)69,
            (char)70, (char)71, (char)72, (char)73, (char)74, (char)75, (char)76, (char)77, (char)78, (char)79,
            (char)80, (char)81, (char)82, (char)83, (char)84, (char)85, (char)86, (char)87, (char)88, (char)89,
            (char)90, (char)91, (char)92, (char)93, (char)94, (char)95, (char)96, (char)97, (char)98, (char)99,
            (char)100, (char)101, (char)102, (char)103, (char)104, (char)105, (char)106, (char)107, (char)108, (char)109,
            (char)110, (char)111, (char)112, (char)113, (char)114, (char)115, (char)116, (char)117, (char)118, (char)119,
            (char)120, (char)121, (char)122, (char)123, (char)124, (char)125, (char)126, (char)127,
        };

        /// <summary>
        /// Control chars
        /// </summary>
        public static readonly char[] ControlChars = new[]
        {
            (char)0, (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9,
            (char)10, (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19,
            (char)20, (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29,
            (char)30, (char)31,
        };

        /// <summary>
        /// Symbol chars
        /// </summary>
        public static readonly char[] SymbolChars = new[]
        {
            ' ', '!', '"', '#', '$', '%', '&', '\'',    // 32~39
            '(', ')', '*', '+', ',', '-', '.', '/',     // 40~47
            ':', ';', '<', '=', '>', '?', '@',          // 58~64
            '[', '\\', ']', '^', '_', '`',              // 91~96
            '{', '|', '}', '~',                         // 123~126
        };

        /// <summary>
        /// Letter chars
        /// </summary>
        public static readonly char[] LetterChars = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g',
            'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',
        };

        /// <summary>
        /// Lower chars
        /// </summary>
        public static readonly char[] LowerChars = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g',
            'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',
        };

        /// <summary>
        /// Upper chars
        /// </summary>
        public static readonly char[] UpperChars = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z',
        };

        /// <summary>
        /// Number chars
        /// </summary>
        public static readonly char[] NumberChars = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        /// <summary>
        /// Windows illegal filename chars
        /// </summary>
        public static readonly char[] WinIllegalFileNameChars = new[]
        {
            (char)0, (char)1, (char)2, (char)3, (char)4, (char)5, (char)6, (char)7, (char)8, (char)9,
            (char)10, (char)11, (char)12, (char)13, (char)14, (char)15, (char)16, (char)17, (char)18, (char)19,
            (char)20, (char)21, (char)22, (char)23, (char)24, (char)25, (char)26, (char)27, (char)28, (char)29,
            (char)30, (char)31,
            '\\', '/', ':', '*', '?', '"', '<', '>', '|'
        };

        /// <summary>
        /// Linux illegal filename chars
        /// </summary>
        public static readonly char[] LinuxIllegalFilenameChars = new[] { (char)0, '/' };


        /// <summary>
        /// Null
        /// </summary>
        public const char NUL = (char)0;

        /// <summary>
        /// Start of headline
        /// </summary>
        public const char SOH = (char)1;

        /// <summary>
        /// Start of text
        /// </summary>
        public const char STX = (char)2;

        /// <summary>
        /// End of text
        /// </summary>
        public const char ETX = (char)3;

        /// <summary>
        /// End of transmission
        /// </summary>
        public const char EOT = (char)4;

        /// <summary>
        /// Enquiry
        /// </summary>
        public const char ENQ = (char)5;

        /// <summary>
        /// Acknowledge
        /// </summary>
        public const char ACK = (char)6;

        /// <summary>
        /// Bell
        /// </summary>
        public const char BEL = (char)7;

        /// <summary>
        /// Backspace
        /// </summary>
        public const char BS = (char)8;

        /// <summary>
        /// Horizontal tab
        /// </summary>
        public const char HT = (char)9;

        /// <summary>
        /// Line feed / New line
        /// </summary>
        public const char LF = (char)10;

        /// <summary>
        /// Vertical tab
        /// </summary>
        public const char VT = (char)11;

        /// <summary>
        /// Form feed / New Page
        /// </summary>
        public const char FF = (char)12;

        /// <summary>
        /// Carriage return
        /// </summary>
        public const char CR = (char)13;

        /// <summary>
        /// Shift out
        /// </summary>
        public const char SO = (char)14;

        /// <summary>
        /// Shift in
        /// </summary>
        public const char SI = (char)15;

        /// <summary>
        /// Data link escape
        /// </summary>
        public const char DLE = (char)16;

        /// <summary>
        /// Device control 1
        /// </summary>
        public const char DC1 = (char)17;

        /// <summary>
        /// Device control 2
        /// </summary>
        public const char DC2 = (char)18;

        /// <summary>
        /// Device control 3
        /// </summary>
        public const char DC3 = (char)19;

        /// <summary>
        /// Device control 4
        /// </summary>
        public const char DC4 = (char)20;

        /// <summary>
        /// Negative acknowledge
        /// </summary>
        public const char NAK = (char)21;

        /// <summary>
        /// Synchronous idle
        /// </summary>
        public const char SYN = (char)22;

        /// <summary>
        /// End of transmission block
        /// </summary>
        public const char ETB = (char)23;

        /// <summary>
        /// Cancel
        /// </summary>
        public const char CAN = (char)24;

        /// <summary>
        /// End of medium
        /// </summary>
        public const char EM = (char)25;

        /// <summary>
        /// Substitute
        /// </summary>
        public const char SUB = (char)26;

        /// <summary>
        /// Escape
        /// </summary>
        public const char ESC = (char)27;

        /// <summary>
        /// File separator
        /// </summary>
        public const char FS = (char)28;

        /// <summary>
        /// Group separator
        /// </summary>
        public const char GS = (char)29;

        /// <summary>
        /// Record separator
        /// </summary>
        public const char RS = (char)30;

        /// <summary>
        /// Unit separator
        /// </summary>
        public const char US = (char)31;

    }

}
