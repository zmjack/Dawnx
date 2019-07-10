using Linqx;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DawnxDemo.Data
{
    public class ApplicationDbContext : LinqxDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OneClass> OneClasses { get; set; }

    }
}
