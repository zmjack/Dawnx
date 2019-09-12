using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NLinq.Test
{
    public class CPKeyModel
    {
        [CPKey(1)]
        public Guid Id1 { get; set; }

        [CPKey(2)]
        public Guid Id2 { get; set; }

    }
}
