using Xunit;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dawnx.Reflection;
using Dawnx.Entity;
using System.Globalization;
using Dawnx.Utilities;
using System.Reflection;
using SimpleData;

namespace Dawnx.AspNetCore.Test
{
    public class ToSqlTests
    {
        private DbContextOptions MySqlOptions = new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1").Options;
        private DbContextOptions SqlServerOptions = new DbContextOptionsBuilder().UseSqlServer("Server=127.0.0.1").Options;
        private DbContextOptions SqliteOptions = SimpleSources.NorthwndOptions;
        
        [Fact]
        public void Test1()
        {
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                var query = sqlite.Employees
                    .WhereSearch("London", e => new
                    {
                        ProductName = e.Orders
                            .SelectMany(o => o.Order_Details)
                            .Select(x => x.Product.ProductName),
                        ShipCountry = e.Orders.Select(x => x.ShipCountry),
                        ShipRegion = e.Orders.Select(x => x.ShipRegion),
                        ShipCity = e.Orders.Select(x => x.ShipCity),
                        ShipAddress = e.Orders.Select(x => x.ShipAddress),
                    });
                var sql = query.ToSql();
            }

            var now = DateTime.Now.AddDays(-1).AddHours(-2);

            using (var mysql = new NorthwndContext(MySqlOptions))
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                var employees_WhoSelled_AllKindsOfTofu = sqlite.Employees
                    .WhereSearch("Tofu", e => new
                    {
                        ProductName = e.Orders
                            .SelectMany(o => o.Order_Details)
                            .Select(x => x.Product.ProductName)
                    });
                var sql1 = employees_WhoSelled_AllKindsOfTofu.ToSql();

                var employees_WhoSelled_Tofu = sqlite.Employees
                     .WhereMatch("Tofu", e => new
                     {
                         ProductName = e.Orders
                             .SelectMany(o => o.Order_Details)
                             .Select(x => x.Product.ProductName)
                     });
                var sql2 = employees_WhoSelled_Tofu.ToSql();

                var employees_WhoSelled_LongLifeTofu = sqlite.Employees
                     .WhereMatch("Longlife Tofu", e => new
                     {
                         ProductName = e.Orders
                             .SelectMany(o => o.Order_Details)
                             .Select(x => x.Product.ProductName)
                     });
                var sql3 = employees_WhoSelled_LongLifeTofu.ToSql();
            }
            return;

            //var sqls = new[]
            //{
            //    sqlserver.SimpleModels.WhereNot(x=>x.NickName == "zmjack").ToSql(),
            //    sqlserver.SimpleModels.WhereBetween(x => x.Birthday, x => x.Birthday, x => x.Birthday).ToSql(),
            //    sqlserver.SimpleModels.WhereBetween(x => x.Birthday, x => now, now).ToSql(),
            //    sqlserver.SimpleModels.WhereAfter(x => x.Birthday, x => now).ToSql(),
            //    //mysql.SimpleModels.Where(x => x.Birthday < now).ToSql(),
            //    //sqlserver.SimpleModels.Where(x => x.Birthday < now).ToSql(),
            //};

            //var s = mysql.SimpleModels.WhereMatch("Bill", x => new { x.Age, x.NickName }).ToSql();
        }

    }
}
