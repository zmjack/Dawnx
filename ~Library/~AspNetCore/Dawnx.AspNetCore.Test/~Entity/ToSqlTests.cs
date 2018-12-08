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
                new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1;Database=db;uid=username;pwd=password").Options))
            {
                var now = DateTime.Now;
                var s = context.SimpleModels.WhereSearch("Bill", x => new { x.Age, x.Name }).ToSql();
            }
        }

    }
}
