using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDatabase.Data.SimpleDatabase
{
    public class EmployeeTerritory
    {
        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [StringLength(20)]
        [ForeignKey(nameof(Territory))]
        public string TerritoryID { get; set; }
        public Territory Territory { get; set; }
    }
}
