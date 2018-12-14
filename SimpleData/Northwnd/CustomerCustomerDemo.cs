using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleData.Northwnd
{
    public class CustomerCustomerDemo
    {
        [StringLength(5)]
        [ForeignKey(nameof(Customer))]
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        [StringLength(10)]
        [ForeignKey(nameof(CustomerDemographic))]
        public string CustomerTypeID { get; set; }
        public CustomerDemographic CustomerDemographic { get; set; }
    }
}
