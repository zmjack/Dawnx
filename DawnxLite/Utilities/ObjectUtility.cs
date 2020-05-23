using NStandard;
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
                else dict[prop.Name] = null;
            }

            return dict;
        }

        private static object GetConvertedValue(List<object> stateContainer, object value)
        {
            if (value.GetType().IsBasicType())
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

                if (prop.PropertyType.IsBasicType(true))
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

                    if (prop.PropertyType.IsBasicType(true))
                        dict.Add($"{prefix}{prop.Name}", value);
                    else Write(value, ignorePrefix ? string.Empty : $"{prop.Name}_");
                }
            }

            Write(instance, string.Empty);
            return dict;
        }

    }
}