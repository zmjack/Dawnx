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
        class DB : DbContext
        {
            public DB()
                : base(new DbContextOptionsBuilder()
                .UseSqlite("filename=C:/Users/19558/Documents/aa.db").Options)
            //.UseMySql("server=.").Options)
            {
            }

            public DbSet<Test> Test { get; set; }
        }

        public class Test : IEntity
        {
            [Key]
            public int Id { get; set; }

            public string Type { get; set; }

            public int Data { get; set; }
        }

        static void Main(string[] args)
        {
            using (var sqlite = new DB())
            {
                //var max = sqlite.Test.Max(y => y.Data);
                //var query = sqlite.Test.Where(x => x.Data == max);

                var max = sqlite.Test
                    .GroupBy(x => x.Type)
                    .Select(g => new
                    {
                        g.Key,
                        max_Data = g.Max(x => x.Data),
                    }).Select(x => $"{x.Key.ToString()} {x.max_Data}");


                var query = sqlite.Test
                    .Where(x => max.Contains(x.Type.ToString() + " " + x.Data));

                //var query = sqlite.Test.GroupBy(x => new { x.Type, x.Data }).Select(g => new
                //{
                //    g.Key,
                //    Max = g.Max(gx => gx.Data),
                //});


                var result = query.ToArray();
                Console.WriteLine(query.ToSql());
                Console.WriteLine(result.Select(x => x.Id).Join(" "));
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
