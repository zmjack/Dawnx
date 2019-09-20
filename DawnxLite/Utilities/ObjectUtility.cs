using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dawnx.Utilities
{
    public static class ObjectUtility
    {
        public static Dictionary<string, object> CovertToDictionary(object obj)
        {
            if (obj is null) return new Dictionary<string, object>();

            var stateContainer = new List<object>();
            return Convert(stateContainer, obj);
        }

        private static Dictionary<string, object> Convert(List<object> stateContainer, object obj)
        {
            if (stateContainer.Any(x => x.GetType().IsClass && !(x is string) && x == obj))
                throw new InvalidDataException("The specified object has as least one circulation path.");

            stateContainer.Add(obj);

            var props = obj.GetType().GetProperties();
            var dict = new Dictionary<string, object>();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value != null)
                    dict[prop.Name] = GetConvertedValue(stateContainer, value);
            }

            return dict;
        }

        private static object GetConvertedValue(List<object> stateContainer, object value)
        {
            if (BasicTypeUtility.IsBasicType(value))
                return value;
            else if (value.GetType().GetInterfaces().Contains(typeof(IEnumerable)))
                return (value as IEnumerable).OfType<object>().Select(x => GetConvertedValue(stateContainer, x)).ToArray();
            else return Convert(stateContainer, value);
        }

        /// <summary>
        /// Gets a <see cref="Dictionary{String, Object}"/> from an anonymouse type.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyDictionary(object instance)
        {
            var dict = new Dictionary<string, object>();
            var props = instance.GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(instance);

                if (BasicTypeUtility.IsBasicType(prop.PropertyType, true))
                    dict.Add(prop.Name, value);
                else dict.Add(prop.Name, GetPropertyDictionary(value));
            }

            return dict;
        }

        /// <summary>
        /// Gets a <see cref="Dictionary{String, Object}"/> from an anonymouse type.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="ignorePrefix"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyPureDictionary(object instance, bool ignorePrefix = false)
        {
            var dict = new Dictionary<string, object>();

            void Write(object obj, string prefix)
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(obj);

                    if (BasicTypeUtility.IsBasicType(prop.PropertyType, true))
                        dict.Add($"{prefix}{prop.Name}", value);
                    else Write(value, ignorePrefix ? string.Empty : $"{prop.Name}_");
                }
            }

            Write(instance, string.Empty);
            return dict;
        }

        public static Type GetNullableType(object instance) => GetNullableType(instance.GetType().FullName);
        public static Type GetNullableType(Type type) => GetNullableType(type.FullName);
        public static Type GetNullableType(string typeFullName)
        {
            switch (typeFullName)
            {
                case BasicTypeUtility.@bool: return typeof(bool?);
                case BasicTypeUtility.@byte: return typeof(byte?);
                case BasicTypeUtility.@sbyte: return typeof(sbyte?);
                case BasicTypeUtility.@char: return typeof(char?);
                case BasicTypeUtility.@short: return typeof(short?);
                case BasicTypeUtility.@ushort: return typeof(ushort?);
                case BasicTypeUtility.@int: return typeof(int?);
                case BasicTypeUtility.@uint: return typeof(uint?);
                case BasicTypeUtility.@long: return typeof(long?);
                case BasicTypeUtility.@ulong: return typeof(ulong?);
                case BasicTypeUtility.@float: return typeof(float?);
                case BasicTypeUtility.@double: return typeof(double?);
                case BasicTypeUtility.@decimal: return typeof(decimal?);
                case BasicTypeUtility.DateTime: return typeof(DateTime?);
                case BasicTypeUtility.Guid: return typeof(Guid?);
                default: throw new NotSupportedException();
            }
        }

    }
}