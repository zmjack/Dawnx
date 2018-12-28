using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class ObjectUtilityTests
    {
        [Fact]
        public void AnonymousTypeTest1()
        {
            Assert.Equal(new[] { "a", "b" }, ObjectUtility.GetPropertyDictionary(new
            {
                a = 1,
                b = 2
            }).Keys);
        }

        [Fact]
        public void AnonymousTypeTest2()
        {
            Assert.Equal(new[] { "a", "b" }, ObjectUtility.GetPropertyDictionary(new
            {
                a = 1,
                b = 2
            }).Keys);

            var anonymous = new
            {
                a = 1,
                b = 2,
                c = new
                {
                    RegionID = 5,
                }
            };
            Assert.Equal(new[] { "a", "b", "RegionID" }, ObjectUtility.GetPropertyPureDictionary(anonymous, true).Keys);
            Assert.Equal(new[] { "a", "b", "c_RegionID" }, ObjectUtility.GetPropertyPureDictionary(anonymous, false).Keys);
        }

    }
}
