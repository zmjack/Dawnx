using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore
{
    public class JsonFormat
    {
        public enum JsonFormatQuote { DoubleQuote, SingleQuote }

        /// <summary>
        /// Gets or sets a value indicating whether object names will be surrounded with quotes.
        /// </summary>
        public bool QuoteName { get; set; } = true;

        /// <summary>
        /// Gets or sets which character to use to quote attribute values.
        /// </summary>
        public JsonFormatQuote QuoteChar { get; set; } = JsonFormatQuote.DoubleQuote;

        /// <summary>
        /// Gets or sets how many IndentChars to write for each level in the hierarchy.
        /// </summary>
        public int Indentation { get; set; } = 2;

        /// <summary>
        /// Gets or sets which character to use for indenting.
        /// </summary>
        public char IndentChar { get; set; } = ' ';

        public static JsonFormat Default = new JsonFormat();
    }
}
