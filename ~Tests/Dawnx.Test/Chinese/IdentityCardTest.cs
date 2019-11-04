using NStandard;
using System;
using Xunit;

namespace Dawnx.Chinese.Test
{
    public class IdentityCardTest
    {
        [Fact]
        public void Test1()
        {
            new IdentityCard("110000197001010014").Then(correctCard =>
            {
                Assert.True(correctCard.IsValid);
                Assert.Equal(new DateTime(1970, 1, 1), correctCard.Birthday);
            });

            new IdentityCard("110000197001010015").Then(correctCard =>
            {
                Assert.False(correctCard.IsValid);
                Assert.Equal(new DateTime(1970, 1, 1), correctCard.Birthday);
            });

        }

    }
}
