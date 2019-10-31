using System;
using Xunit;

namespace Dawnx.Test
{
    public class DawnTypeTests
    {
        private interface InterfaceA<T> { }
        private interface InterfaceB { }
        private class ClassA<T> : InterfaceA<T> { }
        private class ClassB : ClassA<int>, InterfaceB { }
        private class ClassC : ClassB { }

        [Fact]
        public void Test1()
        {
            Assert.True(typeof(ClassC).IsImplement<InterfaceA<int>>());
            Assert.True(typeof(ClassC).IsImplement(typeof(InterfaceA<int>)));
            Assert.True(typeof(ClassC).IsImplementGeneric(typeof(InterfaceA<int>)));
            Assert.True(typeof(ClassC).IsImplementGeneric(typeof(InterfaceA<>)));

            Assert.True(typeof(ClassC).IsImplement<InterfaceB>());

            Assert.False(typeof(ClassC).IsExtend<ClassA<int>>());
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<int>)));
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<>)));
            Assert.False(typeof(ClassC).IsExtendGeneric(typeof(ClassA<>)));

            Assert.True(typeof(ClassC).IsExtend<ClassA<int>>(true));
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassA<int>), true));
            Assert.False(typeof(ClassC).IsExtend(typeof(ClassA<>), true));
            Assert.True(typeof(ClassC).IsExtendGeneric(typeof(ClassA<>), true));

            Assert.True(typeof(ClassC).IsExtend<ClassB>());
            Assert.True(typeof(ClassC).IsExtend(typeof(ClassB)));
            Assert.ThrowsAny<ArgumentException>(() => typeof(ClassC).IsExtendGeneric(typeof(ClassB)));
        }

    }
}
