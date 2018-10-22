using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.Entity
{
    public interface IEntityMonitor
    {
        string MonitorExecutor { get; set; }

        void OnAdd(IEnumerable<PropertyEntry> entries);
        void OnModity(IEnumerable<PropertyEntry> entries);
        void OnDelete(IEnumerable<PropertyEntry> entries);
    }

}
