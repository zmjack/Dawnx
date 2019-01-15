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
using Dawnx.Net.Web;
using Dawnx.Net.Web.Processors;

namespace DawnxDevloping
{
    class Program
    {

        static void Main(string[] args)
        {
            var web = new HttpAccess();
            web.ClearProcessors();
            web.AddProcessor(new RedirectProcessor().Self(_ => _.OnRedirect += __OnRedirect));
            var s = web.Get("http://aka.ms");

            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
            }

        }

        private static void __OnRedirect(string location)
        {
            Console.WriteLine(location);
        }
    }
}
#endif
