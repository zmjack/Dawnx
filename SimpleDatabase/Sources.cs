using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SimpleDatabase
{
    public static class Sources
    {
        private static readonly string NugetPackagesPath
            = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + @"\.nuget\packages";

        public static readonly string Northwnd_ConnectionString
            = $@"filename={NugetPackagesPath}\simpledatabase\{Config.Assembly.VERSION}\sources\northwnd.db";
    }
}
