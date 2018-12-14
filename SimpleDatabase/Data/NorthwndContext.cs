using Microsoft.EntityFrameworkCore;
using SimpleDatabase.Data.SimpleDatabase;

namespace SimpleDatabase.Data
{
    public class NorthwndContext : DbContext
    {
        public NorthwndContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order_Detail> Order_Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

        public void WriteTo(NorthwndContext targetContext)
        {
            //var s = targetContext.Database.EnsureCreated();
            targetContext.Database.Migrate();

            using (var trans = targetContext.Database.BeginTransaction())
            {
                targetContext.AddRange(Regions);
                targetContext.SaveChanges();

                targetContext.AddRange(Territories);
                targetContext.SaveChanges();

                targetContext.AddRange(Employees);
                targetContext.SaveChanges();

                targetContext.AddRange(EmployeeTerritories);
                targetContext.SaveChanges();

                targetContext.AddRange(Categories);
                targetContext.SaveChanges();

                targetContext.AddRange(Suppliers);
                targetContext.SaveChanges();

                targetContext.AddRange(Products);
                targetContext.SaveChanges();

                targetContext.AddRange(Shippers);
                targetContext.SaveChanges();

                targetContext.AddRange(CustomerDemographics);
                targetContext.SaveChanges();

                targetContext.AddRange(Customers);
                targetContext.SaveChanges();

                targetContext.AddRange(CustomerCustomerDemos);
                targetContext.SaveChanges();

                targetContext.AddRange(Orders);
                targetContext.SaveChanges();

                targetContext.AddRange(Order_Details);
                targetContext.SaveChanges();

                trans.Commit();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerCustomerDemo>().HasKey(e => new { e.CustomerTypeID, e.CustomerID });
            modelBuilder.Entity<EmployeeTerritory>().HasKey(e => new { e.EmployeeID, e.TerritoryID });
            modelBuilder.Entity<Order_Detail>().HasKey(e => new { e.OrderID, e.ProductID });

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Subordinates)
                .WithOne(e => e.Superordinate)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Details)
                .WithOne(e => e.Order)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Details)
                .WithOne(e => e.Product)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Territories)
                .WithOne(e => e.Region)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
