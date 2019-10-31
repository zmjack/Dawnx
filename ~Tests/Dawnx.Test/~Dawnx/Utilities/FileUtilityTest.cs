using Dawnx.Utilities;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class FileUtilityTest
    {
        [Fact]
        public void Test1()
        {
            Assert.False(FileUtility.IsFilePath(@"\"));
            Assert.False(FileUtility.IsFilePath(@"\1|3.txt"));
            Assert.False(FileUtility.IsFilePath(@"\1:3.txt"));
            Assert.False(FileUtility.IsFilePath(@"\1*3.txt"));
            Assert.False(FileUtility.IsFilePath(@"\1""3.txt"));
            Assert.False(FileUtility.IsFilePath(@"\1<3.txt"));
            Assert.False(FileUtility.IsFilePath(@"\1>3.txt"));

            Assert.True(FileUtility.IsFilePath(@"\123.txt"));
            Assert.True(FileUtility.IsFilePath(@"\1(3.txt"));
            Assert.True(FileUtility.IsFilePath(@"\1)3.txt"));
        }

    }
}
