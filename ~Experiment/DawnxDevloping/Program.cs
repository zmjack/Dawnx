#if !USE
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Dawnx.Algorithms.StringAlgorithm;
using SimpleData;
using Dawnx;
using Dawnx.AspNetCore;

namespace DawnxDevloping
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var sql = sqlite.Employees
                    .WhereSearch("QUICK", x => x.Orders.Select(o => o.CustomerID))
                    .ToSql();

                var s = sqlite.Products
                    .WhereSearch(new[] { "Tofu", "pkg" }, x => new { x.ProductName, x.QuantityPerUnit });
                Console.WriteLine(s.ToSql());
                Console.WriteLine(s.Select(x => x.ProductName).ToArray().Join(","));
            }

        }

    }
}
#endif
