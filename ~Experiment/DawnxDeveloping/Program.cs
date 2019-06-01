#if !USE
using Dawnx.AspNetCore;
using Dawnx.AspNetCore.Identity;
using System;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            var item = new SimpleData.Northwnd.Category
            {
                CategoryID = 111,
                CategoryName = "123",
            };
            var s = item.Json(JsonFormat.Default);

            return;


            var hash = PasswordHasher.ComputeHash("123");
            var ret = PasswordHasher.Verify(hash, "123");
            Console.WriteLine(hash);
            Console.WriteLine(ret);
        }

    }
}
#endif
