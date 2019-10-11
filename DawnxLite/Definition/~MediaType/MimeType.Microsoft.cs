namespace Dawnx.Definition
{
    public static partial class MimeType
    {
        public static class Microsoft
        {
            // Refer: https://blogs.msdn.microsoft.com/vsofficedeveloper/2008/05/08/office-2007-file-format-mime-types-for-http-content-streaming-2/
            public const string WORD_2003 = APPLICATION_MSWORD;
            public const string EXCEL_2003 = APPLICATION_VND_MS_EXCEL;
            public const string PPT_2003 = APPLICATION_VND_MS_POWERPOINT;

            public const string WORD_2007 = APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT;
            public const string EXCEL_2007 = APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET;
            public const string PPT_2007 = APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION;
        }

    }

}
