#if USE
using Dawnx;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using Dawnx.Net.Web;
using System.IO;
using Dawnx.CConsole;

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

            ConsoleUtility.CreateSeamlessTable(
                new[] { "A", "B", "C" },
                new string[][]
                {
                    new []{ "a123", "1一二三四五六", "c34567" },
                },
                new[] { 2, 4, 6 });

            ConsoleUtility.CreateBorderTable(models);
            ConsoleUtility.CreateSeamlessTable(models);
            ConsoleUtility.CreateNoBorderTable(models);
        }

    }
}
#endif
