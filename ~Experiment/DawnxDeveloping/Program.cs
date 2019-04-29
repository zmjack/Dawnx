#if !USE

using Dawnx.AspNetCore.Identity;
using System;
using System.Reflection;

namespace DawnxDevloping
{


    class Program
    {
        static void Main(string[] args)
        {
            return;


            var hash = PasswordHasher.ComputeHash("123");
            var ret = PasswordHasher.Verify(hash, "123");
            Console.WriteLine(hash);
            Console.WriteLine(ret);
        }

    }
}
#endif
