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
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Dawnx.Entity;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            {
                //var max = mysql.Order_Details.Max(y => y.UnitPrice);
                //var query = mysql.Order_Details.Where(x => x.UnitPrice == max);

                var max = mysql.Order_Details
                    .GroupBy(x => x.OrderID)
                    .Select(g => new
                    {
                        g.Key,
                        max_Data = g.Max(x => x.UnitPrice),
                    }).ToArray().Select(x => $"{x.Key.ToString()} {x.max_Data}");
                //Console.WriteLine(max);

                //max = sqlite.Order_Details
                //    .GroupBy(x => x.OrderID)
                //    .Select(g => new
                //    {
                //        g.Key,
                //        max_Data = g.Max(x => x.UnitPrice),
                //    }).ToArray().Select(x => $"{x.Key.ToString()} {x.max_Data}");

                var query = mysql.Order_Details
                    .Where(x => max.Contains(x.OrderID + " " + x.UnitPrice));

                //var query = sqlite.Test.GroupBy(x => new { x.Type, x.Data }).Select(g => new
                //{
                //    g.Key,
                //    Max = g.Max(gx => gx.Data),
                //});

                Console.WriteLine(query.ToSql());

                //var result = query.ToArray();
                //Console.WriteLine(query.ToSql());
                //Console.WriteLine(result.Count());
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
