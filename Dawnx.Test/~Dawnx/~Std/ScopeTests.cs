using Dawnx.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Dawnx.Test
{
    public class StringScope : Scope<string, StringScope>
    {
        public StringScope(string model) : base(model) { }
    }

    public class ScopeTests
    {
        [Fact]
        public void NestTest()
        {
            Assert.Null(StringScope.Current);
            using (new StringScope("outter"))
            {
                Assert.Equal("outter", StringScope.Current.Model);

                using (new StringScope("inner"))
                {
                    Assert.Equal("inner", StringScope.Current.Model);
                    Assert.Equal(2, StringScope.Scopes.Count);
                }
            }
        }

        [Fact]
        public void ConcurrencyTest()
        {
            Concurrency.Run(() =>
            {
                Assert.Null(StringScope.Current);
                using (new StringScope("outter"))
                {
                    Assert.Equal("outter", StringScope.Current.Model);

                    using (new StringScope("inner"))
                    {
                        Assert.Equal("inner", StringScope.Current.Model);
                        Assert.Equal(2, StringScope.Scopes.Count);
                    }
                }
            }, level: 20, threadCount: 4);
        }

    }
}
