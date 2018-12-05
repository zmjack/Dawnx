using Dawnx;
using Dawnx.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sapling
{
    public class SpTableSource<TModel> : ISpTableSource
    {
        internal SpTableSource() { }
        internal SpTableSource(PagedEnumerable<TModel> models)
            : this(models, models.PageNumber, models.PageSize, models.PageCount) { }
        internal SpTableSource(IEnumerable<TModel> models)
            : this(models, 1, models.Count(), 1) { }
        internal SpTableSource(IEnumerable<TModel> models, int page, int pageSize, int pageCount)
        {
            var props = typeof(TModel).GetProperties();
            var keyProp = props.SingleOrDefault(x => x.GetCustomAttribute<KeyAttribute>() != null);

            Page = page;
            PageSize = pageSize;
            PageCount = pageCount;
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
            Rows = models.Select(x =>
            {
                var key = keyProp?.GetValue(x).ToString() ?? "";
                var ret = new Dictionary<string, string>();
                foreach (var prop in props)
                    ret.Add(prop.Name, DataAnnotationUtility.GetDisplayString(x, prop.Name));

                return new KeyValuePair<string, Dictionary<string, string>>(key, ret);
            }).ToArray();
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string[] Headers { get; set; }
        public string[] Types { get; set; }
        public KeyValuePair<string, Dictionary<string, string>>[] Rows
        {
            get => (this as ISpTableSource).Rows as KeyValuePair<string, Dictionary<string, string>>[];
            set => (this as ISpTableSource).Rows = value;
        }
        object ISpTableSource.Rows { get; set; }
    }
}
