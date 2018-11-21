using System;
using System.IO;

namespace Dawnx.Definition
{
    /// <summary>
    /// MIME Type definitions
    /// (Referrer: https://www.iana.org/assignments/media-types/media-types.xhtml)
    /// </summary>
    public static partial class MimeType
    {
        //TODO: Need to fill this
        [Obsolete("Devloping")]
        public static string Parse(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".html":
                case ".htm": return TEXT_HTML;
                default: return APPLICATION_OCTET_STREAM;
            }
        }
    }
}
