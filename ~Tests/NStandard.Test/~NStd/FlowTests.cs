using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class FlowTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Flow(StringFlows.Base64));
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".Flow(StringFlows.UrlSafeBase64));
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".Flow(StringFlows.FromBase64));
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".Flow(StringFlows.FromUrlSafeBase64));

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Flow(StringFlows.Base64));
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".Flow(StringFlows.UrlSafeBase64));
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".Flow(StringFlows.FromBase64));
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".Flow(StringFlows.FromUrlSafeBase64));

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Flow(StringFlows.Base64));
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".Flow(StringFlows.UrlSafeBase64));
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".Flow(StringFlows.FromBase64));
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".Flow(StringFlows.FromUrlSafeBase64));
        }

    }
}
