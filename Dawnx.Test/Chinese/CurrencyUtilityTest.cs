using Xunit;

namespace Dawnx.Chinese.Test
{
    public class CurrencyUtilityTest
    {
        [Fact]
        public void IntegerLowerTest()
        {
            Assert.Equal("十万〇一元整", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = true,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万〇一元整", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万〇一百〇一元整", CurrencyUtility.GetString(10_0101, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万一千〇一元整", CurrencyUtility.GetString(10_1001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("一十万一千〇一十元整", CurrencyUtility.GetString(10_1010, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Lower,
            }));
        }

        [Fact]
        public void IntegerUpperTest()
        {
            Assert.Equal("拾万零壹元整", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = true,
                Target = CurrencyOption.ETarget.Upper,
            }));

            Assert.Equal("壹拾万零壹元整", CurrencyUtility.GetString(10_0001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Upper,
            }));

            Assert.Equal("壹拾万零壹佰零壹元整", CurrencyUtility.GetString(10_0101, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Upper,
            }));

            Assert.Equal("壹拾万壹仟零壹元整", CurrencyUtility.GetString(10_1001, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Upper,
            }));

            Assert.Equal("壹拾万壹仟零壹拾元整", CurrencyUtility.GetString(10_1010, new CurrencyOption
            {
                IsSimplified = false,
                Target = CurrencyOption.ETarget.Upper,
            }));
        }

        [Fact]
        public void DoubleTest()
        {
            Assert.Equal("十万〇一元二角整", CurrencyUtility.GetString(10_0001.2, new CurrencyOption
            {
                IsSimplified = true,
                Target = CurrencyOption.ETarget.Lower,
            }));

            Assert.Equal("十万〇一元二角三分", CurrencyUtility.GetString(10_0001.23, new CurrencyOption
            {
                IsSimplified = true,
                Target = CurrencyOption.ETarget.Lower,
            }));
        }

    }
}
