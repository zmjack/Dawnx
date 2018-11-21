using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx
{
    public class ConsoleUtility
    {
        private const string TABLE_CELL_PADDING = " ";
        private const int TABLE_CELL_PADDING_LENGTH = 1;

        /// <summary>
        /// Gets the console length.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetConsoleLength(string text)
        {
            int ret = 0;
            bool @continue = false;
            foreach (var ch in text)
            {
                if (ch < 128) ret += 1;
                else { ret += 2; }
            }
            return ret;
        }

        /// <summary>
        /// Gets the count of double bytes char.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetCountOfDoubleBytesChar(string text)
        {
            int ret = 0;
            bool @continue = false;
            foreach (var ch in text)
                if (ch >= 128) ret++;
            return ret;
        }

        /// <summary>
        /// Gets table line, like ┌┬┐(specified by the format value).
        /// </summary>
        /// <param name="lengths"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private static void PrintTableLine(int[] lengths, string format, object[] data = null)
        {
            var ubound = lengths.GetUpperBound(0);

            for (int i = 0; i < lengths.Length; i++)
            {
                if (i == 0) Console.Write(format[0]);
                else Console.Write(format[1]);

                if (!(data is null))
                {
                    Console.Write(TABLE_CELL_PADDING);

                    var value = data[i];
                    if (value is ConsoleValue)
                    {
                        if (!((value as ConsoleValue).BackgroundColor is null))
                            Console.BackgroundColor = (value as ConsoleValue).BackgroundColor.Value;
                        if (!((value as ConsoleValue).ForegroundColor is null))
                            Console.ForegroundColor = (value as ConsoleValue).ForegroundColor.Value;

                        var svalue = (value as ConsoleValue).Value;
                        Console.Write(svalue.PadRight(lengths[i] - GetCountOfDoubleBytesChar(svalue)));
                        Console.ResetColor();
                    }
                    else
                    {
                        var svalue = value.ToString();
                        Console.Write(svalue.PadRight(lengths[i] - GetCountOfDoubleBytesChar(svalue)));
                    }

                    Console.Write(TABLE_CELL_PADDING);
                }
                else
                {
                    Console.Write("─".Replicate(lengths[i] + TABLE_CELL_PADDING_LENGTH * 2));
                }

                if (i == ubound) Console.Write(format[2]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prints console table for models.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="models"></param>
        public static void PrintTable<TModel>(IEnumerable<TModel> models)
        {
            var props = typeof(TModel).GetProperties();
            var lengths = new int[props.Length];
            var line = new StringBuilder();

            //calculate lengths of each column
            foreach (var prop in props.AsVI())
                lengths[prop.Index] = GetConsoleLength(prop.Value.Name);

            foreach (var prop in props.AsVI())
            {
                foreach (var model in models)
                {
                    var len = GetConsoleLength(prop.Value.GetValue(model).ToString());
                    if (len > lengths[prop.Index])
                        lengths[prop.Index] = len;
                }
            }

            //print lines
            PrintTableLine(lengths, "┌┬┐");
            PrintTableLine(lengths, "│││", props.Select(x => new ConsoleValue
            {
                ForegroundColor = ConsoleColor.Cyan,
                Value = x.Name,
            }).ToArray());

            if (models.Any())
                PrintTableLine(lengths, "├┼┤");

            foreach (var model in models)
                PrintTableLine(lengths, "│││", props.Select(x => x.GetValue(model)).ToArray());

            PrintTableLine(lengths, "└┴┘");
        }

    }
}
