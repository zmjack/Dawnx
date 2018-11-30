using Dawnx.Definition;
using Xunit;

namespace Dawnx.Test.Definition
{
    public class LanguageTest
    {
        [Fact]
        public void ChooseLanguage()
        {
            Assert.True("你好".IsMatch($"^{Unicode.Chinese}+$"));
            Assert.True("こんにちは".IsMatch($"^{Unicode.Japanese}+$"));
            Assert.True("안녕".IsMatch($"^{Unicode.Korean}+$"));
        }

    }
}
