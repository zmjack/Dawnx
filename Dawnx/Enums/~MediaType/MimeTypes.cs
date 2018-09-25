using System.IO;

namespace Dawnx.Enums
{
    /// <summary>
    /// MIME Type definitions
    /// (Referrer: https://www.iana.org/assignments/media-types/media-types.xhtml)
    /// </summary>
    public static partial class MediaType
    {
        public static string GetFrom(string filePath)
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
