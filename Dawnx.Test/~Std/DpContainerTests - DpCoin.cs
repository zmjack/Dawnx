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
            private int[] CoinValues = new[] { 1, 2, 4, 5 };

            public override (int CoinCount, int[] Coins) StateTransfer(int n)
            {
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
            var dpCoinResult = new DpCoin()[8];
            Assert.Equal(2, dpCoinResult.CoinCount);
            Assert.Equal(new[] { 4, 4 }, dpCoinResult.Coins);
        }

    }
}
