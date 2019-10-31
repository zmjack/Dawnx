using NLinq;
using System;
using System.ComponentModel.DataAnnotations;

namespace DawnxDemo.Data
{
    public class OneClass : IEntityTracker<ApplicationDbContext, OneClass>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(IndexType.Unique)]
        public string UserName { get; set; }

        [Provider(typeof(PasswordProvider))]
        public string Password { get; set; }

        [Required]
        [Index(IndexType.Unique, Group = "UniqueA")]
        public string UniqueA1 { get; set; }

        public string A => UniqueA1;

        [Required]
        [Index(IndexType.Unique, Group = "UniqueA")]
        public string UniqueA2 { get; set; }

        [Required]
        [Index(IndexType.Unique, Group = "UniqueB")]
        public DateTime UniqueB1 { get; set; }

        [Required]
        [Index(IndexType.Unique, Group = "UniqueB")]
        public DateTime UniqueB2 { get; set; }

        public void OnDeleting(ApplicationDbContext context)
        {
        }

        public void OnInserting(ApplicationDbContext context)
        {
        }

        public void OnUpdating(ApplicationDbContext context, OneClass origin)
        {
        }

        private class PasswordProvider : IProvider<string, int>
        {
            public override string ConvertFromProvider(int provider) => provider == 0 ? "" : "a";
            public override int ConvertToProvider(string model) => model.Substring(0, 1) == "a" ? 1 : 0;
        }

    }

}
