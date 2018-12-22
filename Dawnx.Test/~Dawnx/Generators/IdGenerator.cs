using Dawnx.Diagnostics;
using Dawnx.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dawnx.Test.Generators
{
    public class IdGenerator
    {
        [Fact]
        public void Test1()
        {
            var generator = new IdGenerator<string>(() => DateTime.Now.Ticks.ToString());
            var level = 2000;

            var result = Concurrency.Run(() =>
            {
                return generator.TakeOne();
            }, level: level);

            Assert.Equal(level, result.Count);
            Assert.Equal(level, result.Select(x => x.Value).Distinct().Count());
        }
    }
}
