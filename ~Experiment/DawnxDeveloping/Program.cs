#if USE

using Dawnx.AspNetCore.Identity;
using System;

namespace DawnxDevloping
{


    class Program
    {
        static void Main(string[] args)
        {
            var hash = PasswordHasher.ComputeHash("123");
            var ret = PasswordHasher.Verify(hash, "123");
            Console.WriteLine(hash);
            Console.WriteLine(ret);
        }

    }
}
#endif
