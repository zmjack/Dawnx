using Newtonsoft.Json;
using System.IO;

namespace Dawnx.AspNetCore
{
    public static partial class DawnObject
    {
        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Json(this object @this) => JsonConvert.SerializeObject(@this);

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="jsonFormat"></param>
        /// <returns></returns>
        public static string Json(this object @this, JsonFormat jsonFormat)
        {
            var serializer = new JsonSerializer();
            var writer = new StringWriter();
            var jsonWriter = new JsonTextWriter(writer)
            {
                Formatting = Formatting.Indented,
                Indentation = jsonFormat.Indentation,
                IndentChar = jsonFormat.IndentChar,
                QuoteName = jsonFormat.QuoteName,
                QuoteChar = jsonFormat.QuoteChar switch
                {
                    JsonFormat.JsonFormatQuote.DoubleQuote => '"',
                    JsonFormat.JsonFormatQuote.SingleQuote => '\'',
                    _ => '\'',
                },
            };
            serializer.Serialize(jsonWriter, @this);

            return writer.ToString();
        }
    }
}
