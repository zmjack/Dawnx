using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SimpleDatabase
{
    public class NorthwndContextFactory : IDesignTimeDbContextFactory<NorthwndContext>
    {
        public NorthwndContext CreateDbContext(string[] args)
        {
            return new NorthwndContext(new DbContextOptionsBuilder()
                .UseSqlite("filename=sources\\northwnd.db").Options);
        }
    }

}
