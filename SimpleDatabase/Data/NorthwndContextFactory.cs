using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDatabase.Data
{
    public class NorthwndContextFactory : IDesignTimeDbContextFactory<NorthwndContext>
    {
        public NorthwndContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NorthwndContext>();
            optionsBuilder.UseSqlite("filename=SimpleDatabase.db");

            return new NorthwndContext(optionsBuilder.Options);
        }
    }

}
