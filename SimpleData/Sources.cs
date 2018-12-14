using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SimpleData
{
    public static class SimpleSources
    {
        private static readonly string NugetPackagesPath
            = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + @"\.nuget\packages";

        public static readonly string NorthwndSource = $@"filename={NugetPackagesPath}\simpledata\{Config.Assembly.VERSION}\sources\northwnd.db";
        public static readonly DbContextOptions NorthwndOptions = new DbContextOptionsBuilder().UseSqlite(NorthwndSource).Options;

    }
}
