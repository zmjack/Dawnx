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
                : base(new DbContextOptionsBuilder().UseSqlite("filename=C:/Users/19558/Documents/aa.db").Options)
            {
            }

            public DbSet<Test> Test { get; set; }
        }

        public class Test : IEntity
        {
            [Key]
            public int Id { get; set; }

            public string Type { get; set; }

            public string Data { get; set; }
        }

        static void Main(string[] args)
        {
            using (var sqlite = new DB())
            {
                var query = sqlite.Test.TryDelete(x => x.Type == "C");
                Console.WriteLine(query.ToSql());
                Console.WriteLine(query.Execute());
            }

            //using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            //{
            //    var query = sqlite.Orders
            //        .TryUpdate(x => x.Order_Details.Any(y => y.OrderID == 10248))
            //        .Set(x => x.ShipCity, "Reims");
            //    var sql = query.ToSql();
            //    //var result = query.ToArray();

            //    Console.WriteLine(sql);
            //    Console.WriteLine(query.Execute());
            //}
        }

    }
}
#endif
