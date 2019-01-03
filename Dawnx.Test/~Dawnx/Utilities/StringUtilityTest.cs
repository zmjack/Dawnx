using Dawnx.Utilities;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class StringUtilityTest
    {
        public class Simple
        {
            public int A { get; set; }
            public double B { get; set; }
            public string C { get; set; }
            public string D;
        }

        [Fact]
        public void CommonStartsTest()
        {
            Assert.Equal("AB", StringUtility.CommonStarts("ABC", "AB123", "ABC23"));
        }

        [Fact]
        public void ReverseProjecTest()
        {
            var simple = new Simple();
            StringUtility.ReverseProject("1|2|3.1|3.2|4", simple, x => $"{x.A}?|{x.B}?|{x.C}|{x.D}");

            Assert.Equal(1, simple.A);
            Assert.Equal(2, simple.B);
            Assert.Equal("3.1|3.2", simple.C);
            Assert.Equal("4", simple.D);
        }

    }
}
