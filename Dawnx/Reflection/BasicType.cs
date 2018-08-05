using System;
using System.Reflection;

namespace Dawnx.Reflection
{
    public static class BasicType
    {
        public const string @bool = nameof(Boolean);
        public const string @byte = nameof(Byte);
        public const string @sbyte = nameof(SByte);
        public const string @char = nameof(Char);
        public const string @short = nameof(Int16);
        public const string @ushort = nameof(UInt16);
        public const string @int = nameof(Int32);
        public const string @uint = nameof(UInt32);
        public const string @long = nameof(Int64);
        public const string @ulong = nameof(UInt64);
        public const string @float = nameof(Single);
        public const string @double = nameof(Double);
        public const string @string = nameof(String);
        public const string @decimal = nameof(Decimal);
        public const string @DateTime = nameof(System.DateTime);

        public static readonly string[] AllNames = new string[]
        {
            @bool, @byte, @sbyte, @char,
            @short, @ushort,
            @int, @uint,
            @long, @ulong,
            @float, @double,
            @string, @decimal, DateTime
        };

        public static readonly string[] AllFullNames = new string[]
        {
            "System.Boolean", "System.Byte", "System.SByte", "System.Char",
            "System.Int16", "System.UInt16",
            "System.Int32", "System.UInt32",
            "System.Int64", "System.UInt64",
            "System.Single", "System.Double",
            "System.String", "System.Decimal", "System.DateTime",
        };

        public static MethodInfo GetToStringMethod(Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean": return typeof(bool).GetMethod("ToString");
                case "System.Byte": return typeof(byte).GetMethod("ToString");
                case "System.SByte": return typeof(sbyte).GetMethod("ToString");
                case "System.Char": return typeof(char).GetMethod("ToString");
                case "System.Int16": return typeof(short).GetMethod("ToString");
                case "System.UInt16": return typeof(ushort).GetMethod("ToString");
                case "System.Int32": return typeof(int).GetMethod("ToString");
                case "System.UInt32": return typeof(uint).GetMethod("ToString");
                case "System.Int64": return typeof(long).GetMethod("ToString");
                case "System.UInt64": return typeof(ulong).GetMethod("ToString");
                case "System.Single": return typeof(float).GetMethod("ToString");
                case "System.Double": return typeof(double).GetMethod("ToString");
                case "System.String": return typeof(string).GetMethod("ToString");
                case "System.Decimal": return typeof(Decimal).GetMethod("ToString");
                case "System.DateTime": return typeof(DateTime).GetMethod("ToString");
                default: throw new NotSupportedException();
            }
        }

    }
}
