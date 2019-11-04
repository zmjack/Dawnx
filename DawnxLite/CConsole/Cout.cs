using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.CConsole
{
    public partial class Cout
    {
        internal Cout()
        {
        }

        public Cout ClearRow()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(" ".Repeat(Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            return this;
        }

        public Cout RowBeginning()
        {
            Console.Write('\b');
            return this;
        }

        public Cout RowEnd()
        {
            Console.SetCursorPosition(Console.WindowWidth, Console.CursorTop);
            return this;
        }

        public Cout Line(int number = 1)
        {
            for (int i = 0; i < number; i++)
                Console.WriteLine();
            return this;
        }

        public Cout Line(string content, ConColor color = null)
        {
            Print(content, color);
            Console.WriteLine();
            return this;
        }

        public Cout Print(string content, ConColor color = null)
        {
            void Process() { Console.Write(content); }

            if (color is null) Process();
            else
            {
                var originColor = ConColor;
                ConColor = color;
                Process();
                ConColor = originColor;
            }

            return this;
        }

        public Cout Left(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{line}{" ".Repeat(Console.WindowWidth - line.GetLengthA())}", color);
            return this;
        }

        public Cout Right(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{" ".Repeat(Console.WindowWidth - line.GetLengthA())}{line}", color);
            return this;
        }

        public Cout Center(string line, ConColor color = null)
        {
            RowBeginning();
            Print($"{line.Center(Console.WindowWidth)}", color);
            return this;
        }

        public Cout Row(string[] cols, int[] colLengths)
        {
            var top = Console.CursorTop;

            ClearRow();
            Console.Write(ConUtility.CreateRow(cols, colLengths));

            if (Console.CursorTop != top)
                Console.CursorTop = top;

            return this;
        }

        public Cout Offset(int offsetRow, int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(_ => _ >= 0 ? _ : 0);
            var top = (Console.CursorTop - offsetRow).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(left, top);
            return this;
        }

        public Cout OffsetRow(int offsetRow)
        {
            var top = (Console.CursorTop - offsetRow).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(Console.CursorLeft, top);
            return this;
        }

        public Cout OffsetCol(int offsetCol)
        {
            var left = (Console.CursorLeft - offsetCol).For(_ => _ >= 0 ? _ : 0);
            Console.SetCursorPosition(left, Console.CursorTop);
            return this;
        }

        public Cout BorderTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.CreateBorderTable(models));
            return this;
        }
        public Cout BorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.CreateBorderTable(headers, colLines, lengths));
            return this;
        }

        public Cout NoBorderTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.CreateNoBorderTable(models));
            return this;
        }
        public Cout NoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.CreateNoBorderTable(headers, colLines, lengths));
            return this;
        }

        public Cout SeamlessTable<TModel>(IEnumerable<TModel> models)
        {
            Console.Write(ConUtility.CreateSeamlessTable(models));
            return this;
        }
        public Cout SeamlessTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.Write(ConUtility.CreateSeamlessTable(headers, colLines, lengths));
            return this;
        }

        public Cout Ask(string question, Func<string, string> resolver)
        {
            new CAsk(this, question, new CAsk.ResolveDelegate(resolver)).Resolve();
            return this;
        }

        public Cout AskYN(string question, Func<bool, string> resolver)
        {
            new CAsk(this, question, new CAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                    return resolver(true);
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                    return resolver(false);
                else return null;
            })).Resolve();

            return this;
        }
        public Cout AskYN(string question, Action<bool> method)
        {
            new CAsk(this, question, new CAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                {
                    method(true);
                    return "Yes";
                }
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                {
                    method(false);
                    return "No";
                }
                else return null;
            })).Resolve();

            return this;
        }
        public Cout AskYN(string question, out bool ret)
        {
            bool _ret = false;
            new CAsk(this, question, new CAsk.ResolveDelegate((answer) =>
            {
                if (new[] { "y", "yes", "Y", "Yes", "YES" }.Contains(answer))
                {
                    _ret = true;
                    return "Yes";
                }
                else if (new[] { "n", "no", "N", "No", "NO" }.Contains(answer))
                {
                    _ret = false;
                    return "No";
                }
                else return null;
            })).Resolve();

            ret = _ret;
            return this;
        }

    }
}
