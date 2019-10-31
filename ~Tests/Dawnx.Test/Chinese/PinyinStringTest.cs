﻿using Xunit;

namespace Dawnx.Chinese.Test
{
    public class PinyinStringTest
    {
        [Fact]
        public void Test1()
        {
            var pinyin = new PinyinString("欢迎");
            Assert.Equal("HUANYING", pinyin.Pinyin);
            Assert.Equal("HUAN1YING2", pinyin.PinyinWithTone);
        }

    }
}
