#if !USE
using System.Linq;
using System.Collections.Generic;
using System;
using Dawnx.Diagnostics;
using Dawnx.Con;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            Con.Out.BorderTable(
                new[] { "Af f f地", "B", "C" },
                new[]
                {
                    new[] { "1", "2", "3" },
                    new[] { "11", "22", "33" },
                },
                new[] { 10, 20, 30 });

            Con.Out.AskYN("Are you Sure", answer => { })
                .Ask("Project Name", answer =>
                {
                    if (answer == "")
                        Console.WriteLine("aaaa");
                    else Console.WriteLine("ggg");

                    return answer;
                });
        }

    }
}
#endif
