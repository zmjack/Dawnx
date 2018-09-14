using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public class Class1
    {
        [Fact]
        public void Test1()
        {
            var graphics = Graphics.FromImage(new Bitmap(100, 100));

            SizeF sizeF = graphics.MeasureString("地地", new Font("宋体", 9));
            var w = sizeF.Width;
        }
    }
}
