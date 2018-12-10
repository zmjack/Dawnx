using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Definition
{
    // Refer:(2018-12-10) https://docs.microsoft.com/en-us/ef/core/providers/index
    public static class DatabaseProvider
    {
        public static string Cosmos = "Cosmos";
        public static string Firebird = "Firebird";
        public static string IBM = "IBM";
        public static string Jet = "Jet";
        public static string MyCat = "MyCat";
        public static string MySql = "MySql";
        public static string OpenEdge = "OpenEdge";
        public static string Oracle = "Oracle";
        public static string PostgreSQL = "PostgreSQL";
        public static string Sqlite = "Sqlite";
        public static string SqlServer = "SqlServer";
        public static string SqlServerCompact35 = "SqlServerCompact35";
        public static string SqlServerCompact40 = "SqlServerCompact40";
    }
}
