using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;

namespace Dawnx.AspNetCore
{
    [Obsolete("Developing")]
    public interface IEntityMonitor
    {
        void OnModify(IEnumerable<PropertyEntry> entries);
        //void OnDelete();
    }    

}
