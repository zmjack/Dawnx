using System;
using Xunit;

namespace NStd.Test
{
    public class XArrayTest
    {
        private class Class { }

        [Fact]
        public void LetTest1()
        {
            var arr = new int[5].Let(i => i * 2 + 1);
            Assert.Equal(new[] { 1, 3, 5, 7, 9 }, arr);
        }

        [Fact]
        public void LetTest2()
        {
            var classes = new Class[2].Let(() => new Class());
            Assert.NotNull(classes[0]);
            Assert.NotNull(classes[1]);
        }

        [Fact]
        public void LUBoundTest()
        {
            var array = Array.CreateInstance(typeof(int), new[] { 2 }, new[] { 5 });
            Assert.Equal(5, array.LBound());
            Assert.Equal(6, array.UBound());
        }

        [Fact]
        public void ShuffleTest()
        {
            var random = new Random();
            var arr = new int[100].Let(i => i);
            arr.Shuffle();
        }

    }
}
