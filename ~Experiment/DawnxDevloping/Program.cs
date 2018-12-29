#if !USE
using System;
using System.Linq;
using SimpleData;
using Dawnx.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace DawnxDevloping
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(SimpleSources.NorthwndSource);

            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            using (var mysql = new NorthwndContext(new DbContextOptionsBuilder()
                .UseMySql("server=127.0.0.1;database=Northwnd").Options))
            {
                //var max = mysql.Order_Details.Max(y => y.UnitPrice);
                //var query = mysql.Order_Details.Where(x => x.UnitPrice == max);

                var query = sqlite.Employees.Where(x => !x.BirthDate.HasValue);
                var sql = query.ToSql();

                Console.WriteLine(sql);
            }

            //using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            //using (var sqlite = new NorthwndContext(
            //    new DbContextOptionsBuilder().UseSqlite("server=.").Options))
            //{
            //    var query = sqlite.Order_Details
            //        .GroupBy(x => x.OrderID)
            //        .Take(2);
            //    var sql = query.ToSql();
            //    //var result = query.ToArray();

            //    Console.WriteLine(sql);
            //    //Console.WriteLine(query.Save());
            //}
        }

    }
}
#endif
