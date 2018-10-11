using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test._Std
{
    public class DpContainerTests
    {
        public class DpFeb : DpContainer<int, int>
        {
            public override int StateTransfer(int n)
            {
                if (n == 1) return 1;
                if (n == 2) return 2;
                return this[n - 1] + this[n - 2];
            }
        }

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
        public void DpFebTest()
        {
            Assert.Equal(1836311903, new DpFeb()[45]);
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
