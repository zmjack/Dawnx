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
        public void Test1()
        {
            var t1 = Task.Run(() =>
            {
                Assert.Null(StringScope.Current);
                using (var scope1 = new StringScope("123"))
                {
                    Assert.Equal("123", StringScope.Current.Model);

                    using (var scope2 = new StringScope("234"))
                    {
                        Assert.Equal("234", StringScope.Current.Model);
                    }
                }
            });

            var t2 = Task.Run(() =>
            {
                using (var scope1 = new StringScope("111"))
                {
                    Assert.Equal("111", StringScope.Current.Model);

                    using (var scope2 = new StringScope("222"))
                    {
                        Assert.Equal("222", StringScope.Current.Model);
                    }
                }
            });

            Task.WaitAll(t1, t2);
        }

    }
}
