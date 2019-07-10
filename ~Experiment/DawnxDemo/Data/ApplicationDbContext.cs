using Microsoft.EntityFrameworkCore;
using NLinq;

namespace DawnxDemo.Data
{
    public class ApplicationDbContext : LinqxDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OneClass> OneClasses { get; set; }

    }
}
