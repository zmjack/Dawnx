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
        [Fact]
        public void Test1()
        {
            var mysqlOptions = new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1").Options;
            var sqlserver = new DbContextOptionsBuilder().UseSqlServer("Server=127.0.0.1").Options;

            var now = DateTime.Now.AddDays(-1).AddHours(-2);

            using (var mysql = new NorthwndContext(mysqlOptions))
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var sql = sqlite.Employees
                    .Where(e => e.Orders
                        .SelectMany(o => o.Order_Details)
                        .Select(x => x.Product.ProductName)
                        .Contains("Tofu")).ToSql();

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
