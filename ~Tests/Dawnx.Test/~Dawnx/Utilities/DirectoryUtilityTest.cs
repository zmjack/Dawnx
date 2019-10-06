using Dawnx.Utilities;
using System;
using System.Drawing;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class DirectoryUtilityTest
    {
        [Fact]
        public void Test1()
        {
            Assert.True(DirectoryUtility.IsDirectoryPath(@"\"));
            Assert.True(DirectoryUtility.IsDirectoryPath(""));
        }

    }
}
