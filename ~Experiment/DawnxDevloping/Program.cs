#if !USE
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Dawnx.Algorithms.StringAlgorithm;
using SimpleData;
using Dawnx;
using Dawnx.AspNetCore;
using System.Linq.Expressions;
using SimpleData.Northwnd;

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
