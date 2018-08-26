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
            for (var r = 0; r < 360; r++)
                for (var g = 0; g <= 100; g++)
                    for (var b = 0; b <= 100; b++)
                    {
                        var color = Color.FromArgb(0, r, g, b);
                        var ashvColor = ColorUtility.CreateFromAhsv
                            (0, color.GetHue(), color.GetSaturation(), (float)(new[] { r, g, b }.Max(x => x) / 255d));

                        Assert.Equal(color, ashvColor);
                    }
        }

    }
}
