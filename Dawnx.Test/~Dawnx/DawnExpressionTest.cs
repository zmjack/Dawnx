﻿using SimpleData.Northwnd;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public class DawnExpressionTest
    {
        [Fact]
        public void Test()
        {
            var exp = new Expression<Func<Order_Detail, bool>>[]
            {
                 x => x.OrderID == 1,
                 x => x.UnitPrice == 1,
                 x => x.ProductID == 1,
            }.LambdaJoin(Expression.OrElse);
            Assert.Equal("x => (((x.OrderID == 1) OrElse (x.UnitPrice == 1)) OrElse (x.ProductID == 1))", exp.ToString());
        }
    }
}
