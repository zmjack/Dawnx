using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.Test._Std
{
    public class DpContainerTests
    {
        public class DpFeb : DpContainer<int, int>
        {
            public override int StateTransfer(int x)
            {
                if (x == 1) return 1;
                if (x == 2) return 2;
                return this[x - 1] + this[x - 2];
            }
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1836311903, new DpFeb()[45]);
        }
    }
}
