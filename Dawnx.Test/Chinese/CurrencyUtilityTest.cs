using Dawnx.Chinese;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.Chinese.Test
{
    public class CurrencyUtilityTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("十万〇一", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = true,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万〇一", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万〇一百〇一", CurrencyUtility.GetString(10_0101, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万一千〇一", CurrencyUtility.GetString(10_1001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万一千〇一十", CurrencyUtility.GetString(10_1010, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));
        }

    }
}
