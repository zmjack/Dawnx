using System;
using System.Collections.Generic;
using System.Text;

namespace Linqx
{
    public class IndexAttribute : Attribute
    {
        public IndexType Type { get; set; }
        public IndexAttribute(IndexType type) { Type = type; }
    }

}
