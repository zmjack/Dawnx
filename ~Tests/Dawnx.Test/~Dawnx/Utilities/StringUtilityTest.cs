using Dawnx.Utilities;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class StringUtilityTest
    {
        public class Simple
        {
            public int A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D;
        }

        [Fact]
        public void CamelCaseTest()
        {
            Assert.Equal("jackT0", StringUtility.CamelCase("JackT0"));
            Assert.Equal("jackT0", StringUtility.CamelCase("JAckT0"));
            Assert.Equal("jackt0", StringUtility.CamelCase("JACKT0"));
            Assert.Equal("jackT0", StringUtility.CamelCase("jackT0"));
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
            StringUtility.Extract("1||3.1|3.2|45", simple, x => $"{x.A}?|{x.B}?|{x.C}|{x.D}?");

            Assert.Equal(1, simple.A);
            Assert.Equal("", simple.B);
            Assert.Equal("3.1|3.2", simple.C);
            Assert.Equal("45", simple.D);
        }

    }
}
