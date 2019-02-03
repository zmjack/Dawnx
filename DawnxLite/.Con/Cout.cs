using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Con
{
    public partial class Cout
    {
        internal Cout()
        {
        }

        public Cout ClearRow()
        {
            Console.Write('\b');
            Console.Write(" ".Repeat(Console.WindowWidth));
            Console.Write('\b');
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

        public Cout Write(string content, ConColor color = null)
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

        public Cout WriteLine(string content, ConColor color = null)
        {
            void Process() { Console.WriteLine(content); }

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
            Write($"{line}{Console.WindowWidth - line.GetLengthA()}", color);
            return this;
        }

        public Cout Right(string line, ConColor color = null)
        {
            RowBeginning();
            Write($"{Console.WindowWidth - line.GetLengthA()}{line}", color);
            return this;
        }

        public Cout Center(string line, ConColor color = null)
        {
            RowBeginning();
            Write($"{line.Center(Console.WindowWidth)}", color);
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
            Console.WriteLine(ConUtility.CreateBorderTable(models));
            return this;
        }
        public Cout BorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.WriteLine(ConUtility.CreateBorderTable(headers, colLines, lengths));
            return this;
        }

        public Cout NoBorderTable<TModel>(IEnumerable<TModel> models)
        {
            Console.WriteLine(ConUtility.CreateNoBorderTable(models));
            return this;
        }
        public Cout NoBorderTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.WriteLine(ConUtility.CreateNoBorderTable(headers, colLines, lengths));
            return this;
        }

        public Cout SeamlessTable<TModel>(IEnumerable<TModel> models)
        {
            Console.WriteLine(ConUtility.CreateSeamlessTable(models));
            return this;
        }
        public Cout SeamlessTable(string[] headers, string[][] colLines, int[] lengths)
        {
            Console.WriteLine(ConUtility.CreateSeamlessTable(headers, colLines, lengths));
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

    }
}
