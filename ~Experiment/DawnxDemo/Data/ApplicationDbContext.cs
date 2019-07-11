using Microsoft.EntityFrameworkCore;
using NLinq;

namespace DawnxDemo.Data
{
    public class ApplicationDbContext : NDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OneClass> OneClasses { get; set; }

    }
}
