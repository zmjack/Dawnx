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

        public static readonly string[] NumberFullNames_AddNullables = NumberFullNames.Select(x =>
            $"System.Nullable`1[[{x}, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]").ToArray();
        public static readonly string[] NumberArrayFullNames_AddNullables = NumberFullNames_AddNullables.Select(x => $"{x}[]").ToArray();

        public static bool IsBasicType(object obj, bool includeNullable = false) => IsBasicType(obj.GetType(), includeNullable);
        public static bool IsBasicType(Type type, bool includeNullable = false)
        {
            var ret = FullNames.Contains(type.FullName) || type.BaseType?.FullName == Enum;

            if (includeNullable)
                return ret || FullNames_AddNullables.Contains(type.FullName);
            else return ret;
        }

        public static bool IsNumberType(object obj, bool includeNullable = false) => IsNumberType(obj.GetType(), includeNullable);
        public static bool IsNumberType(Type type, bool includeNullable = false)
        {
            var ret = NumberFullNames.Contains(type.FullName);

            if (includeNullable)
                return ret || NumberFullNames_AddNullables.Contains(type.FullName);
            else return ret;
        }

        public static bool IsNullableType(object obj) => IsNullableType(obj.GetType());
        public static bool IsNullableType(Type type) => type.FullName.StartsWith("System.Nullable");

        public static MethodInfo GetMethodForToString(Type type)
        {
            switch (type.FullName)
            {
                case @bool: return typeof(bool).GetMethod("ToString");
                case @byte: return typeof(byte).GetMethod("ToString");
                case @sbyte: return typeof(sbyte).GetMethod("ToString");
                case @char: return typeof(char).GetMethod("ToString");
                case @short: return typeof(short).GetMethod("ToString");
                case @ushort: return typeof(ushort).GetMethod("ToString");
                case @int: return typeof(int).GetMethod("ToString");
                case @uint: return typeof(uint).GetMethod("ToString");
                case @long: return typeof(long).GetMethod("ToString");
                case @ulong: return typeof(ulong).GetMethod("ToString");
                case @float: return typeof(float).GetMethod("ToString");
                case @double: return typeof(double).GetMethod("ToString");
                case @string: return typeof(string).GetMethod("ToString");
                case @decimal: return typeof(decimal).GetMethod("ToString");
                case DateTime: return typeof(DateTime).GetMethod("ToString");
                case Guid: return typeof(Guid).GetMethod("ToString");
                default:
                    if (type.BaseType?.FullName == Enum)
                        return typeof(Enum).GetMethod("ToString");
                    else throw new NotSupportedException();
            }

        }

    }
}
