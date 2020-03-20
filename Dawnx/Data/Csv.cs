using Dawnx.Reflection;
using LinqSharp;
using NStandard;
using System;
using System.Data;
using System.Linq;

namespace Dawnx.Data
{
    public class Csv : Csv<BasicConverter>
    {
        public Csv(string source, string separator = ",")
            : base(source, separator) { }
    }

    public class Csv<TBasicConverter>
        where TBasicConverter : IBasicConverter, new()
    {
        public string Source { get; private set; }
        public string Separator { get; private set; }
        public string[] Titles { get; private set; }
        public string[][] Values { get; private set; }
        private TBasicConverter _Converter = new TBasicConverter();

        private DataTable _Table = new DataTable();
        public DataTable Table { get => _Table.Clone(); }

        public Csv(string source, string separator = ",")
        {
            Source = source.NormalizeNewLine();
            Separator = separator;

            Titles = Source.GetLines().FirstOrDefault()?
                .For(_ => _.Split(new[] { Separator }, StringSplitOptions.None)) ?? new string[0];
            Values = Source.GetLines().Skip(1)
                .Select(_ => _.Split(new[] { Separator }, StringSplitOptions.None)).ToArray();
        }

        public DataTable ToTable()
        {
            var table = new DataTable();
            Titles.Each(title => table.Columns.Add(new DataColumn(title)));
            Values.Each(row => table.Rows.Add(row));
            return table;
        }

        public TRet[] ToArray<TRet>()
            where TRet : new()
        {
            var ret = new TRet[Values.Length].Let(() => new TRet());
            var props = typeof(TRet).GetProperties();

            for (int j = 0; j < Titles.Length; j++)
            {
                var prop = props.FirstOrDefault(
                    p => DataAnnotationEx.GetDisplayName(p) == Titles[j]);

                if (prop != null)
                {
                    for (int i = 0; i < ret.Length; i++)
                    {
                        prop.SetValue(ret[i],
                            _Converter.Convert(prop.PropertyType, Values[i][j], prop));
                    }
                }
            }

            return ret;
        }

    }
}
