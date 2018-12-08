using Xunit;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.Test
{
    public class ToSqlTests
    {
        [Fact]
        public void Test1()
        {
            using (var context = new ApplicationDbContext(
                new DbContextOptionsBuilder().UseSqlServer("Server=localhost;Database=database;uid=username;pwd=password").Options))
            {
                var s = context.SimpleModels.WhereMatch("Bill", x => new { x.Age, x.Name }).ToSql();
            }
        }

    }
}
