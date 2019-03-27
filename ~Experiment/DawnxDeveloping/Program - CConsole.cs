#if !USE
using Dawnx;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
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
