using Dawnx.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount.Entities
{
    public class LiveUserRole : IEntity<LiveUserRole>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(RoleLink))]
        public Guid Role { get; set; }

        [StringLength(127)]
        [ForeignKey(nameof(UserLink))]
        public string User { get; set; }

        public virtual IdentityUser UserLink { get; set; }
        public virtual LiveRole RoleLink { get; set; }

    }
}
