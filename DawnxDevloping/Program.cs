using Dawnx;
using Dawnx.Diagnostics;
using Dawnx.Generators;
using Dawnx.Patterns;
using System;
using System.Linq;
using System.Threading;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            JSend s = JSend.Success.Create(123);
            s.data = "1";
            Console.WriteLine(s.data);
        }

    }
}
