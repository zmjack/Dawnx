using LinqSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount.Entities
{
    public class LiveOperation : IEntity<LiveOperation>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(127)]
        public string Name { get; set; }

        public ICollection<LiveRoleOperation> RoleOperations { get; set; }
        public ICollection<LiveOperationAction> OperationActions { get; set; }
    }
}
