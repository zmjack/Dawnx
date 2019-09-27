using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLinq.ProviderFunctions
{
    public static class PMySql
    {
        [DbFunction]
        public static double Rand() => throw new NotImplementedException();
    }
}
