using System.Reflection;
using Xunit;

namespace Dawnx.Test
{
    public class DawnAssemblyTests
    {
        public interface IA { }

        public class A : IA { }
        public class B : A { }
        public class C : B { }

        [Fact]
        public void Test1()
        {
            Assert.Equal(new[] { typeof(B) }, Assembly.GetExecutingAssembly().GetTypesWhichExtends<A>(false));
            Assert.Equal(new[] { typeof(B), typeof(C) }, Assembly.GetExecutingAssembly().GetTypesWhichExtends<A>(true));

            Assert.Equal(new[] { typeof(A), typeof(B), typeof(C) }, Assembly.GetExecutingAssembly().GetTypesWhichImplements<IA>());
        }

    }
}
