using Xunit;

namespace Dawnx.Test.Utilities
{
    public class Base64UtilityTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Base64Encode());
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".UrlSafeBase64Encode());
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".Base64Decode());
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".UrlSafeBase64Decode());

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Base64Encode());
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".UrlSafeBase64Encode());
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".Base64Decode());
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".UrlSafeBase64Decode());

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Base64Encode());
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".UrlSafeBase64Encode());
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".Base64Decode());
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".UrlSafeBase64Decode());
        }

    }
}
