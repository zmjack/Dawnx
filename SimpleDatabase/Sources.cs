using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SimpleDatabase
{
    public static class SimpleSources
    {
        private static readonly string NugetPackagesPath
            = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + @"\.nuget\packages";

        public static readonly DbContextOptions NorthwndOptions = new DbContextOptionsBuilder()
            .UseSqlite($@"filename={NugetPackagesPath}\simpledatabase\{Config.Assembly.VERSION}\sources\northwnd.db").Options;
    }
}
