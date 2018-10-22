using Dawnx.AspNetCore.Entity;
using Dawnx.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dawnx.AspNetCore.Test
{
    public class SimpleModel : IEntity<SimpleModel>, IEntityMonitor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        [NotMapped]
        public string MonitorExecutor { get; set; }

        public void OnAdd(IEnumerable<PropertyEntry> entries)
        {
            throw new NotImplementedException();
        }

        public void OnModity(IEnumerable<PropertyEntry> entries)
        {
            throw new NotImplementedException();
        }

        public void OnDelete(IEnumerable<PropertyEntry> entries)
        {
            throw new NotImplementedException();
        }
    }
}
