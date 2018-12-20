#if !USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Dawnx.AspNetCore;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var orderValues = new[]
                {
                    "Nancy",
                    "Andrew",
                    "Janet",
                    "Margaret",
                    "Steven",
                    "Michael",
                    "Robert",
                    "Laura",
                    "Anne",
                };

                var query = sqlite.Employees.OrderBy(x => x.FirstName == "Nancy" ? 1 : x.FirstName == "Andrew" ? 2 : 3);
                var sql = query.ToSql();

                var ss1 = sqlite.Employees.OrderByCase(x => x.FirstName, orderValues).ToSql();
                var ss2 = sqlite.Employees.OrderByCaseDescending(x => x.FirstName, orderValues).ToArray();


                foreach (var order in sqlite.Orders)
                    Console.WriteLine(order.ShippedDate);
            }
        }

    }
}
#endif
