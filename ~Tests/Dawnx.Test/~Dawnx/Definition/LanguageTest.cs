using Dawnx.Definition;
using System.Text.RegularExpressions;
using Xunit;

namespace Dawnx.Test.Definition
{
    public class LanguageTest
    {
        [Fact]
        public void ChooseLanguage()
        {
            Assert.True("你好".IsMatch(new Regex($"^{Unicode.Chinese}+$")));
            Assert.True("こんにちは".IsMatch(new Regex($"^{Unicode.Japanese}+$")));
            Assert.True("안녕".IsMatch(new Regex($"^{Unicode.Korean}+$")));
        }

    }
}
