using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public partial class DpContainerTests
    {
        public class DpCoin : DpContainer<int, (int CoinCount, int[] Coins)>
        {
            public int[] CoinValues { get; private set; }

            public DpCoin(int[] coinValues)
            {
                CoinValues = CoinValues;
            }

            public override (int CoinCount, int[] Coins) StateTransfer(int n)
            {
                // dp(n) = 0                    if  n=0
                // dp(n) = min[d(n-v) + 1]      if  n-v>=0, v={CoinValues}

                if (n == 0) return (0, new int[0]);

                var take_v = CoinValues
                    .Where(v => n - v >= 0)
                    .WhereMin(v => this[n - v].CoinCount)
                    .First();
                return this[n - take_v].For(_ => (_.CoinCount + 1, _.Coins.Concat(new[] { take_v }).ToArray()));
            }
        }

        [Fact]
        public void DpCoinTest()
        {
            var dpCoin = new DpCoin(new[] { 1, 2, 4, 5 });
            var result8 = dpCoin[8];

            Assert.Equal(2, result8.CoinCount);
            Assert.Equal(new[] { 4, 4 }, result8.Coins);
        }

    }
}
