#if !USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleDatabase.Data;
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
            using (var context = new NorthwndContext(new DbContextOptionsBuilder()
                .UseSqlite(SimpleDatabase.Sources.Northwnd_ConnectionString).Options))
            {
            }
        }

    }
}
#endif
