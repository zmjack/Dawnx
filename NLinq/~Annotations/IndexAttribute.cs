using System;

namespace NLinq
{
    public class IndexAttribute : Attribute
    {
        public IndexType Type { get; set; }
        public IndexAttribute(IndexType type) { Type = type; }
    }

}
