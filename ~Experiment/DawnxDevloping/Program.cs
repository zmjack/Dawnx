﻿#if !USE
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
                var s = sqlite.Order_Details.Average(x => x.Quantity);
                Console.WriteLine();
            }
        }


    }
}
#endif
