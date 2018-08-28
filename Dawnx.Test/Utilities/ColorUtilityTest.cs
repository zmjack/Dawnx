using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class ColorUtilityTest
    {
        [Fact]
        public void AhsvTest()
        {
            for (var r = 0; r < 256; r++)
                for (var g = 0; g < 256; g++)
                    for (var b = 0; b < 256; b++)
                    {
                        var color = Color.FromArgb(r, g, b);
                        var ashvColor = ColorUtility.CreateFromAhsv
                            (color.GetHueOfHsv(), color.GetSaturationOfHsv(), color.GetValueOfHsv());

                        Assert.Equal(color, ashvColor);
                    }
        }

    }
}
