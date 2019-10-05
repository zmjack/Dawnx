using System;
using System.IO;
using Xunit;

namespace Dawnx.Tools.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestEntry()
        {
            Program.TargetProjectInfo = new TargetProjectInfo
            {
                ProjectRoot = Directory.GetCurrentDirectory(),
                ProjectName = "DawnxDemo.csproj",
                AssemblyName = "DawnxDemo",
                RootNamespace = "DawnxDemo",
                TargetFramework = "netcoreapp2.2",
            };
        }

        private void Test1()
        {

        }

    }
}
