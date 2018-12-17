#if !USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                foreach (var order in context.Orders)
                    Console.WriteLine(order.ShippedDate);
            }
        }

    }
}
#endif
