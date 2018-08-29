﻿using System;
using System.Reflection;

namespace Dawnx.Reflection
{
    public static class BasicType
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

        public static readonly string[] AllFullNames = new string[]
        {
            @bool, @byte, @sbyte, @char,
            @short, @ushort,
            @int, @uint,
            @long, @ulong,
            @float, @double,
            @string, @decimal, DateTime
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
