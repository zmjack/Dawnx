using Dawnx.Utilities;
using System.Net;
using Xunit;

namespace Dawnx.Test
{
    public class DawnIPAddressTests
    {
        [Fact]
        public void IPUtilityTest()
        {
            Assert.Equal(0x2414188f, IPAddress.Parse("143.24.20.36").ToLong());
            Assert.Equal("143.24.20.36", new IPAddress(0x2414188f).ToString());

            Assert.Equal(0x8f181424.ToString(), IPUtility.GetLongString(IPAddress.Parse("143.24.20.36")));
            Assert.Equal(0x8f181425.ToString(), IPUtility.GetLongString(IPAddress.Parse("143.24.20.37")));
            
            Assert.Equal("143.24.20.36", IPAddress.Parse(0x8f181424.ToString()).ToString());
            Assert.Equal("143.24.20.37", IPAddress.Parse(0x8f181425.ToString()).ToString());
        }
    }
}
