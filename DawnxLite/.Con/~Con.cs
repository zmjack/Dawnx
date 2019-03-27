using Dawnx.CConsole;
using System;
using System.Collections.Generic;

namespace Dawnx
{
    public static class Con
    {
        public static Cout Instance = new Cout();

        public static Cout ClearRow() => Instance.ClearRow();
        public static Cout RowBeginning() => Instance.RowBeginning();
        public static Cout RowEnd() => Instance.RowEnd();

        public static Cout Line() => Instance.Line();
        public static Cout Print(string content, ConColor color = null) => Instance.Print(content, color);
        public static Cout Left(string line, ConColor color = null) => Instance.Left(line, color);
        public static Cout Right(string line, ConColor color = null) => Instance.Right(line, color);
        public static Cout Center(string line, ConColor color = null) => Instance.Center(line, color);
        public static Cout Row(string[] cols, int[] colLengths) => Instance.Row(cols, colLengths);

        public static Cout Offset(int offsetRow, int offsetCol) => Instance.Offset(offsetRow, offsetCol);
        public static Cout OffsetRow(int offsetRow) => Instance.OffsetRow(offsetRow);
        public static Cout OffsetCol(int offsetCol) => Instance.OffsetCol(offsetCol);

        public static Cout BorderTable<TModel>(IEnumerable<TModel> models) => Instance.BorderTable(models);
        public static Cout BorderTable(string[] headers, string[][] colLines, int[] lengths) => Instance.BorderTable(headers, colLines, lengths);
        public static Cout NoBorderTable<TModel>(IEnumerable<TModel> models) => Instance.NoBorderTable(models);
        public static Cout NoBorderTable(string[] headers, string[][] colLines, int[] lengths) => Instance.NoBorderTable(headers, colLines, lengths);
        public static Cout SeamlessTable<TModel>(IEnumerable<TModel> models) => Instance.SeamlessTable(models);
        public static Cout SeamlessTable(string[] headers, string[][] colLines, int[] lengths) => Instance.SeamlessTable(headers, colLines, lengths);

        public static Cout Ask(string question, Func<string, string> resolver) => Instance.Ask(question, resolver);
        public static Cout AskYN(string question, Func<bool, string> resolver) => Instance.AskYN(question, resolver);
        public static Cout AskYN(string question, Action<bool> method) => Instance.AskYN(question, method);
        public static Cout AskYN(string question, out bool ret) => Instance.AskYN(question, out ret);

    }
}
