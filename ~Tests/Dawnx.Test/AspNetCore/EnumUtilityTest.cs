using NStandard;
using System;
using System.ComponentModel;
using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class EnumUtilityTest
    {
        private enum Sex
        {
            [DisplayName("Man")]
            Male,
            [DisplayName("Woman")]
            Female,
        }

        [Fact]
        public void Test1()
        {
            EnumUtility.GetSelectList<Sex>().Each(item =>
            {
                switch (Enum.Parse<Sex>(item.Value))
                {
                    case Sex.Male:
                        Assert.Equal("Man", item.Text);
                        break;

                    case Sex.Female:
                        Assert.Equal("Woman", item.Text);
                        break;
                }
            });

            EnumUtility.GetSelectList(Sex.Female).Then(_ =>
            {
                Assert.Equal(Sex.Female.ToString(), _.SelectedValue);
            })
            .Each(item =>
            {
                switch (Enum.Parse<Sex>(item.Value))
                {
                    case Sex.Male:
                        Assert.Equal("Man", item.Text);
                        break;

                    case Sex.Female:
                        Assert.Equal("Woman", item.Text);
                        break;
                }
            });
        }

    }
}
