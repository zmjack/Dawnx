using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dawnx.Utilities
{
    public static class ObjectUtility
    {
        public static Dictionary<string, object> CovertToDictionary(object obj)
        {
            if (obj == null) return new Dictionary<string, object>();

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

    }
}
