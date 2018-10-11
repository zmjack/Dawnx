using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public partial class DpContainerTests
    {
        public class DpKnapsacks : DpContainer<int, int>
        {
            private (int Size, int Value)[] Goods = new[]
            {
                (1, 2)
            };

            public override int StateTransfer(int n)
            {
                //dp[i]=max(dp[i-c[j]])+w[j],dp[i])
                if (n == 0 || n == 1) return 1;
                return this[n - 1] + this[n - 2];
            }
        }

        [Fact]
        public void DpKnapsacksTest()
        {
            var dpCoinResult = new DpCoin()[8];
            Assert.Equal(2, dpCoinResult.CoinCount);
            Assert.Equal(new[] { 4, 4 }, dpCoinResult.Coins);
        }

    }
}
