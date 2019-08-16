using Microsoft.EntityFrameworkCore;
using SimpleData;
using System.Linq;
using Xunit;

namespace NLinq.Test
{
    public class DbTests
    {
        private readonly DbContextOptions MySqlOptions = new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1").Options;
        private readonly DbContextOptions SqliteOptions = SimpleSources.NorthwndOptions;

        [Fact]
        public void Test()
        {
            string sql;
            using (var sqlite = new NorthwndContext(SqliteOptions))
            {
                var s = sqlite.Employees.Where(x => x.FirstName == "123").ToSql();

                var query = IQueryableX.DistinctBy(sqlite.Employees, x => x.FirstName);
                var sql2 = query.ToSql();
            }
        }

        ///// <summary>
        ///// Returns distinct elements from a sequence by using a specified properties to compare values.
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="compare"></param>
        ///// <returns></returns>
        //public static IQueryable<TSource> DistinctByValue<TSource>(IQueryable<TSource> source, Func<TSource, object> compare)
        //    => Queryable.Distinct(source, new ExactEqualityComparer<TSource>(compare));

    }
}
