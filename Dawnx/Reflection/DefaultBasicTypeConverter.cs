using Dawnx.Utilities;
using System;
using System.Reflection;

namespace Dawnx.Reflection
{
    public class DefaultBasicTypeConverter : IBasicTypeConverter
    {
        private bool _strict;

        public DefaultBasicTypeConverter() : this(true) { }
        /// <summary>
        /// If set 'strict', null string will be replaced by default of the source.
        /// </summary>
        /// <param name="strict"></param>
        public DefaultBasicTypeConverter(bool strict) { _strict = strict; }

        public object Convert(PropertyInfo prop, object source) => Convert(prop.PropertyType, source, prop);
        public object Convert(FieldInfo field, object source) => Convert(field.FieldType, source, field);
        public object Convert(Type type, object source, ICustomAttributeProvider provider = null)
        {
            switch (type.FullName)
            {
                case BasicTypeUtility.@bool: return ConvertToBoolean(source, provider);
                case BasicTypeUtility.@byte: return ConvertToByte(source, provider);
                case BasicTypeUtility.@sbyte: return ConvertToSByte(source, provider);
                case BasicTypeUtility.@char: return ConvertToChar(source, provider);
                case BasicTypeUtility.@short: return ConvertToInt16(source, provider);
                case BasicTypeUtility.@ushort: return ConvertToUInt16(source, provider);
                case BasicTypeUtility.@int: return ConvertToInt32(source, provider);
                case BasicTypeUtility.@uint: return ConvertToUInt32(source, provider);
                case BasicTypeUtility.@long: return ConvertToInt64(source, provider);
                case BasicTypeUtility.@ulong: return ConvertToUInt64(source, provider);
                case BasicTypeUtility.@float: return ConvertToSingle(source, provider);
                case BasicTypeUtility.@double: return ConvertToDouble(source, provider);
                case BasicTypeUtility.@decimal: return ConvertToDecimal(source, provider);
                case BasicTypeUtility.@string: return ConvertToString(source, provider);
                case BasicTypeUtility.DateTime: return ConvertToDateTime(source, provider);
                default: return source;
            }
        }

        public object ConvertTo<T>(object source, ICustomAttributeProvider provider, Func<object, T> func)
        {
            if (_strict)
            {
                if (IsNullOrWhiteSpaceString(source)) return default(T);
                else return func(source);
            }
            else
            {
                try { return func(source); }
                catch { return default(T); }
            }
        }

        public virtual object ConvertToBoolean(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToBoolean);
        public virtual object ConvertToByte(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToByte);
        public virtual object ConvertToSByte(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToSByte);
        public virtual object ConvertToChar(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToChar);
        public virtual object ConvertToInt16(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToInt16);
        public virtual object ConvertToUInt16(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToUInt16);
        public virtual object ConvertToInt32(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToInt32);
        public virtual object ConvertToUInt32(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToUInt32);
        public virtual object ConvertToInt64(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToInt64);
        public virtual object ConvertToUInt64(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToUInt64);
        public virtual object ConvertToSingle(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToSingle);
        public virtual object ConvertToDouble(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToDouble);
        public virtual object ConvertToDecimal(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToDecimal);
        public virtual object ConvertToString(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToString);
        public virtual object ConvertToDateTime(object source, ICustomAttributeProvider provider)
            => ConvertTo(source, provider, System.Convert.ToDateTime);

        public bool IsNullOrWhiteSpaceString(object source) => source is string && (source as string).IsNullOrWhiteSpace();

    }
}
