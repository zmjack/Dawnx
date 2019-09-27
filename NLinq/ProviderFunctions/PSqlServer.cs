using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLinq.ProviderFunctions
{
    public static class PSqlServer
    {
        [DbFunction]
        public static double Rand() => throw new NotImplementedException();
    }
}
