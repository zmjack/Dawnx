using Dawnx;
using Dawnx.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sapling
{
    public class SpTable
    {
        public class Config : ISaplingConfig
        {

        }

        public static SpTable<TModel> NewSource<TModel>(IEnumerable<TModel> models)
            => new SpTable<TModel>(models);
    }

    public class SpTable<TModel> : ISpTable
    {
        internal SpTable() { }
        internal SpTable(IEnumerable<TModel> models)
        {
            var props = typeof(TModel).GetProperties();
            var keyProp = props.SingleOrDefault(x => x.GetCustomAttribute<KeyAttribute>() != null);

            Headers = props.Select(x => DataAnnotationUtility.GetDisplayName(x)).ToArray();
            Types = props.Select(x =>
                {
                    var name = x.PropertyType.FullName;
                    switch (name)
                    {
                        case string s when s == BasicTypeUtility.@string: return "String";
                        case string s when s.In(new[]
                        {
                        BasicTypeUtility.@byte,
                        BasicTypeUtility.@sbyte,
                        BasicTypeUtility.@short,
                        BasicTypeUtility.@ushort,
                        BasicTypeUtility.@int,
                        BasicTypeUtility.@uint,
                        BasicTypeUtility.@long,
                        BasicTypeUtility.@ulong,
                        BasicTypeUtility.@float,
                        BasicTypeUtility.@double,
                        BasicTypeUtility.@decimal,
                        }):
                            return "Number";
                        case string s when s == BasicTypeUtility.@bool: return "Boolean";
                        case string s when s.EndsWith("[]"): return "Array";
                        case string s when s == BasicTypeUtility.DateTime: return "Date";
                        default: return "Object";
                    }
                }).ToArray();
            Rows = models
                .Select(x =>
                            {
                                var key = keyProp?.GetValue(x).ToString() ?? "";
                                var ret = new Dictionary<string, string>();
                                foreach (var prop in props)
                                    ret.Add(prop.Name, DataAnnotationUtility.GetDisplayString(x, prop.Name));

                                return new KeyValuePair<string, Dictionary<string, string>>(key, ret);
                            })
                            .ToArray();
        }

        public string[] Headers { get; set; }
        public string[] Types { get; set; }
        public KeyValuePair<string, Dictionary<string, string>>[] Rows
        {
            get => (this as ISpTable).Rows as KeyValuePair<string, Dictionary<string, string>>[];
            set => (this as ISpTable).Rows = value;
        }
        object ISpTable.Rows { get; set; }
    }
}
