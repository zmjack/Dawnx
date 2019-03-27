#if !USE
using Dawnx;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            int colLength1 = 1 * 2 + 1;
            int[] tableLengths = new[] { colLength1, 67 - colLength1, 7 };

            Con.Row(new[]
            {
                $"1234",
                $"| adsfafadfa",
                "0.00%"
            }, tableLengths);
            Thread.Sleep(1000);

            Con.Row(new[]
            {
                $"1234",
                $"| adsfafadfa",
                "20.00%"
            }, tableLengths);
            Thread.Sleep(1000);

            Con.Row(new[]
            {
                $"1234",
                $"| adsfafadfa",
                "100.00%"
            }, tableLengths);
            Thread.Sleep(1000);

            Con.Line();

            return;
            var models = new[]
            {
                new SimpleData.Northwnd.Category
                {
                    CategoryID = 123,
                    CategoryName = "CC",
                    Description = "一二三四五",
                    Picture = new byte[0],
                },
            };

            Con.SeamlessTable(
                new[] { "A", "B", "C" },
                new string[][]
                {
                    new []{ "a123", "1一二三四五六", "c34567" },
                },
                new[] { 2, 4, 6 });

            Con.BorderTable(models);
            Con.SeamlessTable(models);
            Con.NoBorderTable(models);
        }

    }
}
#endif
