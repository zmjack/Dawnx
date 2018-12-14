using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SimpleDatabase.Data.SimpleDatabase
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }
        
        public byte[] Photo { get; set; }
        
        public string Notes { get; set; }

        [ForeignKey(nameof(Superordinate))]
        public int? ReportsTo { get; set; }
        public virtual Employee Superordinate { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
        public IEnumerable<Territory> Territories => EmployeeTerritories.Select(x => x.Territory);
    }
}
