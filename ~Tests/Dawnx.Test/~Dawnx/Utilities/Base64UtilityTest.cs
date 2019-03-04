using Dawnx.Utilities;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class Base64UtilityTest
    {
        [Fact]
        public void Test()
        {
            // QUI+Q0Q/RQ==
            Assert.Equal("QUI-Q0Q_RQ", Base64Utility.ConvertBase64ToUrlBase64("AB>CD?E".Base64Encode()));
            Assert.Equal("AB>CD?E", Base64Utility.ConvertUrlBase64ToBase64("QUI-Q0Q_RQ").Base64Decode());

            // QUI+Q0Q/RUY=
            Assert.Equal("QUI-Q0Q_RUY", Base64Utility.ConvertBase64ToUrlBase64("AB>CD?EF".Base64Encode()));
            Assert.Equal("AB>CD?EF", Base64Utility.ConvertUrlBase64ToBase64("QUI-Q0Q_RUY").Base64Decode());

            // QUI+Q0Q/RUZH
            Assert.Equal("QUI-Q0Q_RUZH", Base64Utility.ConvertBase64ToUrlBase64("AB>CD?EFG".Base64Encode()));
            Assert.Equal("AB>CD?EFG", Base64Utility.ConvertUrlBase64ToBase64("QUI-Q0Q_RUZH").Base64Decode());
        }

    }
}
