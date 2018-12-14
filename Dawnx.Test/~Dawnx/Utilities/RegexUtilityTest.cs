using Dawnx.Utilities;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class RegexUtilityTest
    {
        [Fact]
        public void NumberRangeTest()
        {
            new Regex($"^(?:{RegexUtility.NumberRange(1, 100)})$").Self(_ =>
            {
                AssertNumberRangeTrue(_, new[] { 1, 9, 10, 19, 20, 29, 99 });
                AssertNumberRangeFalse(_, new[] { 0, 101 });
            });

            new Regex($"^(?:{RegexUtility.NumberRange(12, 23)})$").Self(_ =>
            {
                AssertNumberRangeTrue(_, new[] { 12, 19, 21, 22, 23 });
                AssertNumberRangeFalse(_, new[] { 0, 101 });
            });

            new Regex($"^(?:{RegexUtility.NumberRange(123, 123)})$").Self(_ =>
            {
                AssertNumberRangeTrue(_, new[] { 123 });
                AssertNumberRangeFalse(_, new[] { 122, 124, 12, 2000 });
            });

            new Regex($"^(?:{RegexUtility.NumberRange(124, 123)})$").Self(_ =>
            {
                AssertNumberRangeFalse(_, new[] { 122, 123, 124, 12, 2000 });
            });
        }

        [Fact]
        public void IPRangeTest()
        {
            new Regex($"^{RegexUtility.IPRange("192.168.1~2.23~34")}$").Self(_ =>
            {
                AssertIPRangeTrue(_, new[] { "192.168.1.23", "192.168.1.29", "192.168.1.30", "192.168.1.34" });
                AssertIPRangeFalse(_, new[] { "192.168.1.22", "192.168.1.35", "192.168.3.30", "192.168.3.34" });
            });

            new Regex($"^{RegexUtility.IPRange("192.*.1~2.23~34")}$").Self(_ =>
            {
                AssertIPRangeTrue(_, new[] { "192.1.1.23", "192.100.1.29", "192.200.1.30", "192.255.1.34" });
                AssertIPRangeFalse(_, new[] { "192.168.1.22", "192.168.1.35", "192.168.3.30", "192.168.3.34" });
            });

            Assert.Throws<FormatException>(() => RegexUtility.IPRange("192.168.1~2"));
            Assert.Throws<FormatException>(() => RegexUtility.IPRange("192.168.256.1"));
            Assert.Throws<FormatException>(() => RegexUtility.IPRange("192.168.1~2.23~256"));
        }

        private void AssertNumberRangeTrue(Regex regex, int[] values)
        {
            foreach (var value in values)
                Assert.True(regex.Match(value.ToString()).Success);
        }
        private void AssertNumberRangeFalse(Regex regex, int[] values)
        {
            foreach (var value in values)
                Assert.False(regex.Match(value.ToString()).Success);
        }

        private void AssertIPRangeTrue(Regex regex, string[] ips)
        {
            foreach (var ip in ips)
                Assert.True(regex.Match(ip).Success);
        }
        private void AssertIPRangeFalse(Regex regex, string[] ips)
        {
            foreach (var ip in ips)
                Assert.False(regex.Match(ip).Success);
        }

    }
}
