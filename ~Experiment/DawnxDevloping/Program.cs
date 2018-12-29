#if !USE
using System;
using System.Linq;
using SimpleData;
using Dawnx.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DawnxDevloping
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            using (var mysql = new NorthwndContext(new DbContextOptionsBuilder()
                .UseMySql("server=127.0.0.1;database=Northwnd").Options))
            using (var sqlserver = new NorthwndContext(new DbContextOptionsBuilder()
                .UseSqlServer("server=127.0.0.1;database=Northwnd").Options))
            {
                //var max = mysql.Order_Details.Max(y => y.UnitPrice);
                //var query = mysql.Order_Details.Where(x => x.UnitPrice == max);

                var sqls = new[]
                {
                    sqlite.Employees.Where(x => x.BirthDate < DateTime.Now).ToSql(),
                    mysql.Employees.Where(x => x.BirthDate < DateTime.Now).ToSql(),
                    sqlserver.Employees.Where(x => x.BirthDate < DateTime.Now).ToSql(),
                };

                Console.WriteLine(sqls.Join(Environment.NewLine));
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
