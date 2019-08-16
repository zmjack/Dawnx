#if !USE
using DawnxDemo.Data;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System.Linq;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            using (var context = new NorthwndContext(new DbContextOptionsBuilder().UseMySql("server=127.0.0.1;database=tmp1").Options))
            {
                sqlite.WriteTo(context);
            }

        }

    }
}
#endif
