#if !USE
using DawnxDemo.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext(
                new DbContextOptionsBuilder().UseMySql("server=127.0.0.1;database=tmp1").Options))
            {
                var a1 = context.OneClasses.Where(x => x.UserName == "U1");
                var a2 = context.OneClasses.Where(x => x.UserName == "U2");

                context.OneClasses.Add(new OneClass
                {
                    UserName = "U1",
                    Password = "as",
                });
                context.OneClasses.Add(new OneClass
                {
                    UserName = "U2",
                    Password = "123",
                });

                context.SaveChanges();
            }
        }

    }
}
#endif
