using Def;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace NStd.Test
{
    public class XStringTest
    {
        [Fact]
        public void IsMatchTest()
        {
            Assert.True("你好".IsMatch(new Regex($"^{Unicode.Chinese}+$")));
            Assert.True("こんにちは".IsMatch(new Regex($"^{Unicode.Japanese}+$")));
            Assert.True("안녕".IsMatch(new Regex($"^{Unicode.Korean}+$")));
        }

    }
}
