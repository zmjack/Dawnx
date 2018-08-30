using System;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public class DawnStringTests
    {
        [Fact]
        public void Common()
        {
            var ds = "123";

            Assert.Equal("123123123", ds.Times(3));
            Assert.Equal("12", ds.Slice(0, -1));
            Assert.Equal("1", ds.Slice(0, 1));
            Assert.Equal("23", ds.Slice(1));
            Assert.Equal("23", ds.Slice(-2));
            Assert.Throws<IndexOutOfRangeException>(() => ds.Slice(3, 2));

            Assert.Equal('1', ds.CharAt(0));
            Assert.Equal('3', ds.CharAt(-1));

            Assert.Equal("zmjack", "zmjack".Center(5));
            Assert.Equal("zmjack", "zmjack".Center(6));
            Assert.Equal(" zmjack", "zmjack".Center(7));
            Assert.Equal(" zmjack ", "zmjack".Center(8));
        }

        [Fact]
        public void GetBytes()
        {
            var str = "ÀèÃ÷";
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.GetBytes(Encoding.UTF8));
            Assert.Equal(Encoding.UTF8.GetBytes(str), str.GetBytes("utf-8"));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.GetBytes(Encoding.UTF8));
            Assert.NotEqual(Encoding.ASCII.GetBytes(str), str.GetBytes("utf-8"));

            var hexString = "0c66182ec710840065ebaa47c5e6ce90";
            var hexString_Base64 = "MGM2NjE4MmVjNzEwODQwMDY1ZWJhYTQ3YzVlNmNlOTA=";
            var hexString_Bytes = new byte[]
            {
                0x0C, 0x66, 0x18, 0x2E, 0xC7, 0x10, 0x84, 0x00, 0x65, 0xEB, 0xAA, 0x47, 0xC5, 0xE6, 0xCE, 0x90
            };
            Assert.Equal(hexString_Bytes, hexString.GetBytesFromHexString());
            Assert.Equal(hexString, hexString_Bytes.GetHexString());

            Assert.Equal(hexString,
                hexString_Base64.GetBytesFromBase64String().GetString(Encoding.Default));
        }

        [Fact]
        public void NormalizeNewLine()
        {
            Assert.Equal("123456", "123\n456".NormalizeNewLine().Replace(Environment.NewLine, ""));
            Assert.Equal("123456", "123\r\n456".NormalizeNewLine().Replace(Environment.NewLine, ""));
        }

        [Fact]
        public void GetLines()
        {
            string nullString = null;
            Assert.Equal(new string[0], nullString.GetLines());
            Assert.Equal(new[] { "123", "456" }, $"123{Environment.NewLine}456".GetLines());
            Assert.Equal(new[] { "123", "456" }, $"123{Environment.NewLine}456{Environment.NewLine}".GetLines());
            Assert.Equal(new[] { "123", "456", " " }, $"123{Environment.NewLine}456{Environment.NewLine} ".GetLines());
        }

        [Fact]
        public void Unique()
        {
            Assert.Equal("123 456 7890", "  123  456    7890 ".Unique());
            Assert.Equal("123 456 7890", "  123  456 7890".Unique());
        }

    }
}
