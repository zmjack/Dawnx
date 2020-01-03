using System;
using System.Linq;
using System.Reflection;

namespace Dawnx.Utilities
{
    public static class BasicTypeUtility
    {
        public const string @bool = "System.Boolean";
        public const string @byte = "System.Byte";
        public const string @sbyte = "System.SByte";
        public const string @char = "System.Char";
        public const string @short = "System.Int16";
        public const string @ushort = "System.UInt16";
        public const string @int = "System.Int32";
        public const string @uint = "System.UInt32";
        public const string @long = "System.Int64";
        public const string @ulong = "System.UInt64";
        public const string @float = "System.Single";
        public const string @double = "System.Double";
        public const string @string = "System.String";
        public const string @decimal = "System.Decimal";
        public const string DateTime = "System.DateTime";
        public const string Guid = "System.Guid";
        public const string Enum = "System.Enum";

        public static readonly string[] FullNames = new string[]
        {
            @bool, @byte, @sbyte, @char,
            @short, @ushort,
            @int, @uint,
            @long, @ulong,
            @float, @double,
            @string, @decimal,
            DateTime, Guid,
        };
        public static readonly string[] ArrayFullNames = FullNames.Select(x => $"{x}[]").ToArray();

        public static readonly string[] FullNames_AddNullables = FullNames.Select(x =>
            $"System.Nullable`1[[{x}, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]").ToArray();
        public static readonly string[] ArrayFullNames_AddNullables = FullNames_AddNullables.Select(x => $"{x}[]").ToArray();

        public static readonly string[] NumberFullNames = new string[]
        {
            @byte, @sbyte,
            @short, @ushort,
            @int, @uint,
            @long, @ulong,
            @float, @double,
            @decimal,
        };
        public static readonly string[] NumberArrayFullNames = NumberFullNames.Select(x => $"{x}[]").ToArray();

    }
}
