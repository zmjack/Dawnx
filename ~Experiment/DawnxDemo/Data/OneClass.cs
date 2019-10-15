using NLinq;
using System;
using System.ComponentModel.DataAnnotations;

namespace DawnxDemo.Data
{
    public class OneClass
    {
        [Key]
        public Guid Id { get; set; }

        [Index(IndexType.Unique)]
        public string UserName { get; set; }

        [Provider(typeof(PasswordProvider))]
        public string Password { get; set; }

        [Index(IndexType.Unique, Group = "UniqueA")]
        public string UniqueA1 { get; set; }

        [Index(IndexType.Unique, Group = "UniqueA")]
        public string UniqueA2 { get; set; }

        [Index(IndexType.Normal, Group = "NormalA")]
        public string NormalA1 { get; set; }

        [Index(IndexType.Normal, Group = "NormalA")]
        public string NormalA2 { get; set; }

        private class PasswordProvider : IProvider<string, int>
        {
            public override string ConvertFromProvider(int provider) => provider == 0 ? "" : "a";
            public override int ConvertToProvider(string model) => model.Substring(0, 1) == "a" ? 1 : 0;
        }

    }

}
