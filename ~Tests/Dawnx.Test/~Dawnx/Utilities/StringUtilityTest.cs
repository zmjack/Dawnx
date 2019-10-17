using Dawnx.Utilities;
using System;
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
            Assert.Equal("", StringUtility.CamelCase(""));
            Assert.Equal("cpKey", StringUtility.CamelCase("CPKey"));
            Assert.Equal("mySQL", StringUtility.CamelCase("MySQL"));
            Assert.Equal("gate2Name", StringUtility.CamelCase("gate2Name"));
            Assert.Equal("dawnxV2", StringUtility.CamelCase("DAWNXV2"));
            Assert.Throws<ArgumentException>(() => StringUtility.CamelCase("Exception\0"));
        }

        [Fact]
        public void KebabCaseTest()
        {
            Assert.Equal("", StringUtility.KebabCase(""));
            Assert.Equal("cp-key", StringUtility.KebabCase("CPKey"));
            Assert.Equal("my-sql", StringUtility.KebabCase("MySQL"));
            Assert.Equal("gate2-name", StringUtility.KebabCase("gate2Name"));
            Assert.Equal("dawnx-v2", StringUtility.KebabCase("DAWNXV2"));
            Assert.Throws<ArgumentException>(() => StringUtility.KebabCase("Exception\0"));
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
