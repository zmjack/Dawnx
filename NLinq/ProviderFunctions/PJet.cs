using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLinq.ProviderFunctions
{
    public static class PJet
    {
        [DbFunction]
        public static double Rnd() => throw new NotSupportedException();

    }
}
