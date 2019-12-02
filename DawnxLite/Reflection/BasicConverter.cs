using Dawnx.Utilities;
using NStandard;
using System;
using System.Reflection;

namespace Dawnx.Reflection
{
    public class BasicConverter : IBasicConverter
    {
        public object Convert(Type type, object source, ICustomAttributeProvider provider)
        {
            switch (type.FullName)
            {
                case string s when s == typeof(string).FullName: return ConvertToString(source, provider);

                case string s when s == typeof(bool).FullName: return ConvertToBoolean(source, provider);
                case string s when s == typeof(byte).FullName: return ConvertToByte(source, provider);
                case string s when s == typeof(sbyte).FullName: return ConvertToSByte(source, provider);
                case string s when s == typeof(char).FullName: return ConvertToChar(source, provider);
                case string s when s == typeof(short).FullName: return ConvertToInt16(source, provider);
                case string s when s == typeof(ushort).FullName: return ConvertToUInt16(source, provider);
                case string s when s == typeof(int).FullName: return ConvertToInt32(source, provider);
                case string s when s == typeof(uint).FullName: return ConvertToUInt32(source, provider);
                case string s when s == typeof(long).FullName: return ConvertToInt64(source, provider);
                case string s when s == typeof(ulong).FullName: return ConvertToUInt64(source, provider);
                case string s when s == typeof(float).FullName: return ConvertToSingle(source, provider);
                case string s when s == typeof(double).FullName: return ConvertToDouble(source, provider);
                case string s when s == typeof(decimal).FullName: return ConvertToDecimal(source, provider);
                case string s when s == typeof(DateTime).FullName: return ConvertToDateTime(source, provider);
                case string s when s == typeof(Guid).FullName: return ConvertToGuid(source, provider);

                case string s when s == typeof(bool?).FullName: return source is null ? null : ConvertToBoolean(source, provider);
                case string s when s == typeof(byte?).FullName: return source is null ? null : ConvertToByte(source, provider);
                case string s when s == typeof(sbyte?).FullName: return source is null ? null : ConvertToSByte(source, provider);
                case string s when s == typeof(char?).FullName: return source is null ? null : ConvertToChar(source, provider);
                case string s when s == typeof(short?).FullName: return source is null ? null : ConvertToInt16(source, provider);
                case string s when s == typeof(ushort?).FullName: return source is null ? null : ConvertToUInt16(source, provider);
                case string s when s == typeof(int?).FullName: return source is null ? null : ConvertToInt32(source, provider);
                case string s when s == typeof(uint?).FullName: return source is null ? null : ConvertToUInt32(source, provider);
                case string s when s == typeof(long?).FullName: return source is null ? null : ConvertToInt64(source, provider);
                case string s when s == typeof(ulong?).FullName: return source is null ? null : ConvertToUInt64(source, provider);
                case string s when s == typeof(float?).FullName: return source is null ? null : ConvertToSingle(source, provider);
                case string s when s == typeof(double?).FullName: return source is null ? null : ConvertToDouble(source, provider);
                case string s when s == typeof(decimal?).FullName: return source is null ? null : ConvertToDecimal(source, provider);
                case string s when s == typeof(DateTime?).FullName: return source is null ? null : ConvertToDateTime(source, provider);
                case string s when s == typeof(Guid?).FullName: return source is null ? null : ConvertToGuid(source, provider);
                default: return source;
            }
        }

        protected object ConvertTo<T>(object source, Func<object, T> func)
        {
            try { return func(source); }
            catch { return default(T); }
        }

        public virtual object ConvertToBoolean(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToBoolean);
        public virtual object ConvertToByte(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToByte);
        public virtual object ConvertToSByte(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToSByte);
        public virtual object ConvertToChar(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToChar);
        public virtual object ConvertToInt16(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToInt16);
        public virtual object ConvertToUInt16(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToUInt16);
        public virtual object ConvertToInt32(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToInt32);
        public virtual object ConvertToUInt32(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToUInt32);
        public virtual object ConvertToInt64(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToInt64);
        public virtual object ConvertToUInt64(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToUInt64);
        public virtual object ConvertToSingle(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToSingle);
        public virtual object ConvertToDouble(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToDouble);
        public virtual object ConvertToDecimal(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToDecimal);
        public virtual object ConvertToString(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToString);
        public virtual object ConvertToDateTime(object source, ICustomAttributeProvider provider) => ConvertTo(source, System.Convert.ToDateTime);
        public virtual object ConvertToGuid(object source, ICustomAttributeProvider provider) => ConvertTo(source, obj => Guid.Parse(obj.ToString()));

    }
}
