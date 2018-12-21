#if !USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Dawnx.AspNetCore;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                A();
                var st = new System.Diagnostics.StackTrace();
                var frames = st.GetFrames();
                Console.WriteLine(st.GetFrame(0).ToString());
            }
        }

        static void A()
        {
            var st = new System.Diagnostics.StackTrace();
            var frames = st.GetFrames();

            st.GetFrame(0).
        }

    }
}
#endif
