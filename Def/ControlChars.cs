using System;
using System.Collections.Generic;
using System.Text;

namespace Def
{
    public static class ControlChars
    {
        /// <summary>
        /// Backspace character.
        /// </summary>
        public const char Back = '\b';

        /// <summary>
        /// Carriage return character.
        /// </summary>
        public const char Cr = '\r';

        /// <summary>
        /// Carriage return/linefeed character combination.
        /// </summary>
        public const string CrLf = "\r\n";

        public static readonly byte[] CrLfBytes = new[] { (byte)'\r', (byte)'\n' };

        /// <summary>
        /// Not used in Microsoft Windows.
        /// </summary>
        public const char FormFeed = '\f';

        /// <summary>
        /// Linefeed character.
        /// </summary>
        public const char Lf = '\n';

        /// <summary>
        /// Null character.
        /// </summary>
        public const char NullChar = '\0';

        /// <summary>
        /// Tab character.
        /// </summary>
        public const char Tab = '\t';

        /// <summary>
        /// Not useful in Microsoft Windows.
        /// </summary>
        public const char VerticalTab = '\v';

    }
}
