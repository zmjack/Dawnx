#if USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Dawnx.AspNetCore;
using Dawnx.Diagnostics;
using Dawnx.Test;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
            }
        }


    }
}
#endif
