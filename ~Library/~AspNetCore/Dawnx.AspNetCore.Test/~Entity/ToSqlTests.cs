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

namespace Dawnx.AspNetCore.Test
{
    public class ToSqlTests
    {
        [Fact]
        public void Test1()
        {
            string mysqlSql, sqlserverSql;

            var mysql = new ApplicationDbContext(
                new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1;Database=db;uid=username;pwd=password").Options);
            var sqlserver = new ApplicationDbContext(
                new DbContextOptionsBuilder().UseSqlServer("Server=127.0.0.1;Database=db;uid=username;pwd=password").Options);

            var now = DateTime.Now.AddDays(-1).AddHours(-2);

            var sqls = new[]
            {
                sqlserver.SimpleModels.WhereBetween(x => x.Birthday, x => x.Birthday, x => x.Birthday).ToSql(),
                sqlserver.SimpleModels.WhereBetween(x => now, x => x.Birthday, now).ToSql(),
                sqlserver.SimpleModels.WhereAfter(x => x.Birthday, x => now).ToSql(),
                //mysql.SimpleModels.Where(x => x.Birthday < now).ToSql(),
                //sqlserver.SimpleModels.Where(x => x.Birthday < now).ToSql(),
            };

            var s = mysql.SimpleModels.WhereMatch("Bill", x => new { x.Age, x.NickName }).ToSql();
        }

    }
}
