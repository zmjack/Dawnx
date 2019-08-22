#if !USE
using DawnxDemo.Data;
using Microsoft.EntityFrameworkCore;
using NLinq;
using SimpleData;
using System.Linq;

namespace DawnxDevloping
{
    class Program
    {
        private static readonly DbContextOptions MySqlOptions = new DbContextOptionsBuilder().UseMySql("Server=127.0.0.1;database=tmp1").Options;
        private static readonly DbContextOptions SqliteOptions = SimpleSources.NorthwndOptions;

        static void Main(string[] args)
        {
            string sql;
            using (var sqlite = new NorthwndContext(MySqlOptions))
            {
                var s = sqlite.Employees.Where(x => x.FirstName == "123").ToSql();

                var query = IQueryableX.DistinctBy(sqlite.Employees, x => x.City);
                var sql2 = query.ToSql();
                var result = query.ToArray();
            }

        }

    }
}
#endif
