using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SimpleDatabase.Northwnd
{
    public partial class CustomerDemographic
    {
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }
        
        public string CustomerDesc { get; set; }
        
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }

        public IEnumerable<Customer> Customers => CustomerCustomerDemos?.Select(x => x.Customer);
    }
}
