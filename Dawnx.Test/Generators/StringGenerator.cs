using Dawnx.Generators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dawnx.Test.Generators
{
    public class StringGeneratorTest
    {
        [Fact]
        public void Test1()
        {
            var taken = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                var generator = new StringGenerator("$d$d", 0);

                var items = generator.Take(5, taken.ToArray());
                taken.AddRange(items);
            }

            Assert.Equal(Range.Create(100), taken.Select(x => int.Parse(x)).OrderBy(x => x));
        }
    }
}
