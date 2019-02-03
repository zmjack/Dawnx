using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Con
{
    public partial class ConUtility
    {
        /// <summary>
        /// Prints console table for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static string CreateBorderTable<TModel>(IEnumerable<TModel> models)
        {
            var props = typeof(TModel).GetProperties();
            var lengths = new int[props.Length];
            var line = new StringBuilder();

            // Calculate lengths of each column
            foreach (var prop in props.AsVI())
                lengths[prop.Index] = prop.Value.Name.GetLengthA();

            foreach (var prop in props.AsVI())
            {
                foreach (var model in models)
                {
                    var len = prop.Value.GetValue(model)?.ToString().GetLengthA() ?? 0;
                    if (len > lengths[prop.Index])
                        lengths[prop.Index] = len;
                }
            }

            return CreateBorderTable(
                headers: props.Select(x => x.Name).ToArray(),
                colLines: models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray(),
                lengths: lengths);
        }

        /// <summary>
        /// Prints console table for models.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="colLines"></param>
        /// <param name="lengths"></param>
        public static string CreateBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            var sb = new StringBuilder();

            var borderLine = "-";
            var borderCols = new string[lengths.Length];
            for (int i = 0; i < borderCols.Length; i++)
                borderCols[i] = borderLine.Repeat(lengths[i]);

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "+-", "-+-", "-+" },
                TreatDBytesTableLineAsByte = false,
            }));

            if (!(headers is null))
            {
                sb.AppendLine(GetAlignConsoleLine(headers, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "| ", " | ", " |" },
                    TreatDBytesTableLineAsByte = false,
                }));

                if (colLines.Any())
                {
                    sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
                    {
                        Lengths = lengths,
                        Borders = new[] { "+-", "-+-", "-+" },
                        TreatDBytesTableLineAsByte = false,
                    }));
                }
            }

            foreach (var colLine in colLines)
            {
                sb.AppendLine(GetAlignConsoleLine(colLine, new AlignLineOptions
                {
                    Lengths = lengths,
                    Borders = new[] { "| ", " | ", " |" },
                    TreatDBytesTableLineAsByte = false,
                }));
            }

            sb.AppendLine(GetAlignConsoleLine(borderCols, new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "+-", "-+-", "-+" },
                TreatDBytesTableLineAsByte = false,
            }));

            return sb.ToString();
        }

    }
}
