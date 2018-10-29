using Dawnx.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.Test
{
    public class SimpleModel : IEntity<SimpleModel>, IEntityMonitor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        [NotMapped]
        public object MonitorCarry { get; set; }        
    }
}
