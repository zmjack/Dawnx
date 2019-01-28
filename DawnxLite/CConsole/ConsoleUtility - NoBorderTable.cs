using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.CConsole
{
    public static partial class ConsoleUtility
    {
        /// <summary>
        /// Prints console table with no border for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static void PrintNoBorderTable<TModel>(IEnumerable<TModel> models)
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

            PrintNoBorderTable(
                headers: props.Select(x => x.Name).ToArray(),
                colLines: models.Select(model => props.Select(x => x.GetValue(model)?.ToString() ?? "").ToArray()).ToArray(),
                lengths: lengths);
        }

        /// <summary>
        /// Prints console table with no border for models.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="colLines"></param>
        /// <param name="lengths"></param>
        private static void PrintNoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            var options = new AlignLineOptions
            {
                Lengths = lengths,
                Borders = new[] { "", " ", "" },
                TreatDBytesTableLineAsByte = false,
            };

            Console.WriteLine(GetAlignConsoleLine(headers, options));
            foreach (var colLine in colLines)
                Console.WriteLine(GetAlignConsoleLine(colLine, options));
        }

    }
}
