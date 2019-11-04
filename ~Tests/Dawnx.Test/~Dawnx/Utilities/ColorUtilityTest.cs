using NStandard;
using Dawnx.Utilities;
using System;
using System.Drawing;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class ColorUtilityTest
    {
        [Fact]
        public void AhsvTest()
        {
            var random = new Random();

            //Sample Test
            int step(int i) => random.Next(256 - i) % 3 + 1;    // 1,2,3
            //Full Test
            //int step(int value) => value + 1;

            for (var r = 0; r < 256; r += step(r))
                for (var g = 0; g < 256; g += step(g))
                    for (var b = 0; b < 256; b += step(b))
                    {
                        var color = Color.FromArgb(r, g, b);
                        var ashvColor = ColorUtility.CreateFromAhsv
                            (color.GetHueOfHsv(), color.GetSaturationOfHsv(), color.GetValueOfHsv());

                        Assert.Equal(color, ashvColor);
                    }
        }

    }
}
