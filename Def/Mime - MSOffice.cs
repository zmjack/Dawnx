using System;

namespace Def
{
    public static partial class Mime
    {
        public static class MSOffice
        {
            // Refer: https://blogs.msdn.microsoft.com/vsofficedeveloper/2008/05/08/office-2007-file-format-mime-types-for-http-content-streaming-2/
            public const string WORD03 = MimeMap.APPLICATION_MSWORD;
            public const string EXCEL03 = MimeMap.APPLICATION_VND_MS_EXCEL;
            public const string PPT03 = MimeMap.APPLICATION_VND_MS_POWERPOINT;

            public const string WORD07 = MimeMap.APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT;
            public const string EXCEL07 = MimeMap.APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET;
            public const string PPT07 = MimeMap.APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION;
        }
    }
}
