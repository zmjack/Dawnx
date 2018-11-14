namespace Dawnx.Definition
{
    public static class ControlChars
    {
        public const char Back = '\b';
        public const char Cr = '\r';
        public const string CrLf = "\r\n";
        public const char FormFeed = '\f';
        public const char Lf = '\n';
        public const char NullChar = '\0';
        public const char Quote = '"';
        public const char Tab = '\t';
        public const char VerticalTab = '\v';
    }

    public static class ControlBytes
    {
        public const byte Back = (byte)'\b';
        public const byte Cr = (byte)'\r';
        public static readonly byte[] CrLf = new[] { Cr, Lf };
        public const byte FormFeed = (byte)'\f';
        public const byte Lf = (byte)'\n';
        public const byte NullChar = (byte)'\0';
        public const byte Quote = (byte)'"';
        public const byte Tab = (byte)'\t';
        public const byte VerticalTab = (byte)'\v';
    }

}
