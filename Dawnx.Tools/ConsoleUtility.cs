using Dawnx.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Tools
{
    public static class ConsoleUtility
    {
        /// <summary>
        /// Gets the console length.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetConsoleLength(string text)
        {
            int ret = 0;
            foreach (var ch in text)
            {
                if (ch < 128) ret += 1;
                else ret += 2;
            }
            return ret;
        }

        /// <summary>
        /// Prints table line, like ┌┬┐(specified by the format value).
        /// </summary>
        /// <param name="lengths"></param>
        /// <param name="format"></param>
        public static void PrintTableLine(int[] lengths, string format)
            => Console.WriteLine(GetTableLine(lengths, format));

        /// <summary>
        /// Prints table line, like ┌┬┐(specified by the format value).
        /// </summary>
        /// <param name="lengths"></param>
        /// <param name="format"></param>
        public static void PrintTableLine(int[] lengths, string format, string[] data)
            => Console.WriteLine(GetTableLine(lengths, format, data));

        /// <summary>
        /// Gets table line, like ┌┬┐(specified by the format value).
        /// </summary>
        /// <param name="lengths"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetTableLine(int[] lengths, string format)
        {
            var ubound = lengths.GetUpperBound(0);
            var line = new StringBuilder();

            for (int i = 0; i < lengths.Length; i++)
            {
                if (i == 0) line.Append(format[0]);
                else line.Append(format[1]);

                line.Append(" " + "─".Times(lengths[i]));

                if (i == ubound) line.Append(format[2]);
            }

            return line.ToString();
        }

        /// <summary>
        /// Gets table line, like ┌┬┐(specified by the format value).
        /// </summary>
        /// <param name="lengths"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetTableLine(int[] lengths, string format, string[] data)
        {
            var ubound = lengths.GetUpperBound(0);
            var line = new StringBuilder();

            for (int i = 0; i < lengths.Length; i++)
            {
                if (i == 0) line.Append(format[0]);
                else line.Append(format[1]);

                line.Append(" " + data[i].PadRight(lengths[i]));

                if (i == ubound) line.Append(format[2]);
            }

            return line.ToString();
        }

        /// <summary>
        /// Prints console table.
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
                lengths[prop.Index] = GetConsoleLength(NetCompatibility.GetDisplayNameFromAttribute(prop.Value));

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
            PrintTableLine(lengths, "│││", props.Select(x => x.Name).ToArray());

            if (models.Any())
                PrintTableLine(lengths, "├┼┤");

            foreach (var model in models)
                PrintTableLine(lengths, "│││", props.Select(x => x.GetValue(model).ToString()).ToArray());

            PrintTableLine(lengths, "└┴┘");
        }
    }
}
