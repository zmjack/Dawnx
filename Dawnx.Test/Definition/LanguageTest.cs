using Dawnx.Definition;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.Test.Definition
{
    public class LanguageTest
    {
        [Fact]
        public void ChooseLanguage()
        {
            Assert.True("你好".IsMatch($"^{Unicode.Language.Chinese}+$"));
            Assert.True("こんにちは".IsMatch($"^{Unicode.Language.Japanese}+$"));
            Assert.True("안녕".IsMatch($"^{Unicode.Language.Korean}+$"));
        }

    }
}
