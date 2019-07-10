using Linqx;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount.Entities
{
    public class LiveRoleOperation : IEntity<LiveRoleOperation>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(RoleLink))]
        public Guid Role { get; set; }

        [ForeignKey(nameof(OperationLink))]
        public Guid Operation { get; set; }

        public virtual LiveOperation OperationLink { get; set; }
        public virtual LiveRole RoleLink { get; set; }
    }
}
