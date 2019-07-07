#if !USE
using Dawnx.AspNetCore;
using Dawnx.AspNetCore.Identity;
using SimpleData;
using System;
using System.Linq;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var query = sqlite.Employees.Where(x => x.City == "London");
                var sql = query.ToSql();
            }

            var item = new SimpleData.Northwnd.Category
            {
                CategoryID = 111,
                CategoryName = "123",
            };
            var s = item.Json();

            return;


            var hash = PasswordHasher.ComputeHash("123");
            var ret = PasswordHasher.Verify(hash, "123");
            Console.WriteLine(hash);
            Console.WriteLine(ret);
        }

    }
}
#endif
