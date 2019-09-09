using System;

namespace NLinq
{
    public class CompositeKeyAttribute : Attribute
    {
        public int Order { get; set; }
        public CompositeKeyAttribute(int order) { Order = order; }
    }

}
