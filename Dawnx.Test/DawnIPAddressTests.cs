using Dawnx.Utilities;
using System.Net;
using Xunit;

namespace Dawnx.Test
{
    public class DawnIPAddressTests
    {
        [Fact]
        public void GetLongValueOf_IPAddress()
        {
            Assert.Equal(0x2414188f, IPAddress.Parse("143.24.20.36").ToLong());
            Assert.Equal("143.24.20.36", new IPAddress(0x2414188f).ToString());

            Assert.Equal(0x8f181424, IPUtility.GetLongValue(IPAddress.Parse("143.24.20.36")));
            Assert.Equal(0x8f181425, IPUtility.GetLongValue(IPAddress.Parse("143.24.20.37")));

            Assert.Equal(0x8f181424, IPUtility.GetLongValue(IPAddress.Parse("143.24.20.36")));
            Assert.Equal("143.24.20.36", IPUtility.CreateFromLongValue(0x8f181424).ToString());
        }
    }
}
