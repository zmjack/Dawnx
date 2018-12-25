#if !USE
using Dawnx.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Dawnx;
using SimpleData;
using Dawnx.AspNetCore;
using SimpleData.Northwnd;

namespace DawnxDevloping
{
    class Program
    {
        public class EqulityCompare : IEqualityComparer<Order_Detail>
        {
            public bool Equals(Order_Detail x, Order_Detail y)
            {
                return x.Quantity == y.Quantity;
            }

            public int GetHashCode(Order_Detail obj) => 0;
        }

        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var s1 = (from x in sqlite.Order_Details
                          group x by x.Quantity into g
                          where g.Count() > 1
                          select new
                          {
                              q = g.Key
                          }).ToSql();
                var s = sqlite.Order_Details.GroupBy(x => x.Quantity)
                        .Where(x => x.Count() > 1)
                        .Select(g => new { q = g.Key })
                        .ToSql();
                Console.WriteLine();
            }
        }

    }
}
#endif
