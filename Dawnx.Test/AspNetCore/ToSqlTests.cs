using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.Linq;
using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class ToSqlTests
    {
        private readonly DbContextOptions MySqlOptions = new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1").Options;
        private readonly DbContextOptions SqlServerOptions = new DbContextOptionsBuilder().UseSqlServer("Server=127.0.0.1").Options;
        private readonly DbContextOptions SqliteOptions = SimpleSources.NorthwndOptions;

        [Fact]
        public void Test()
        {
            string sql;
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                sql = sqlite.Employees
                    .WhereSearch("Tofu", e => new
                    {
                        ProductName = e.Orders
                            .SelectMany(o => o.Order_Details)
                            .Select(x => x.Product.ProductName)
                    }).ToSql();
            }
        }

        [Fact]
        public void WhereBetweenTest()
        {
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                var query = sqlite.Employees
                    .WhereBetween(x => x.BirthDate, new DateTime(1960, 5, 1), new DateTime(1960, 5, 31));

                var sql = query.ToSql();

                var result = query.ToArray();
                Assert.Single(result);
            }
        }

        [Fact]
        public void WhereTest()
        {
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                var query = sqlite.Employees
                    .WhereAfter(x => x.Country, x => x.EmployeeID, x => x.EmployeeID, DateTime.Now);
                var sql = query.ToSql();

                //var result = query.ToArray();
                //Assert.Single(result);
            }
        }

        [Fact]
        public void OrderByCaseTest1()
        {
            //  RegionID    RegionDescription
            //  1   Eastern
            //  2   Western
            //  3   Northern
            //  4   Southern

            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var originResult = sqlite.Regions;
                var orderedResult =
                    sqlite.Regions.OrderByCase(x => x.RegionDescription, new[] { "Northern", "Eastern", "Western", "Southern" });

                Assert.Equal(new[] { 1, 2, 3, 4 }, originResult.Select(x => x.RegionID));
                Assert.Equal(new[] { 3, 1, 2, 4 }, orderedResult.Select(x => x.RegionID));
            }
        }

        [Fact]
        public void OrderByCaseTest2()
        {
            //  RegionID    RegionDescription
            //  1   Eastern
            //  2   Western
            //  3   Northern
            //  4   Southern

            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var originResult = sqlite.Regions;
                var orderedResult =
                    sqlite.Regions.OrderByCase(x => x.RegionID, new[] { 3, 1, 2, 4 });

                Assert.Equal(new[] { 1, 2, 3, 4 }, originResult.Select(x => x.RegionID));
                Assert.Equal(new[] { 3, 1, 2, 4 }, orderedResult.Select(x => x.RegionID));
            }
        }

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
