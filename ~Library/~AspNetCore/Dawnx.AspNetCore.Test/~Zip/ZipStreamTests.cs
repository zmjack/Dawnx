using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.AspNetCore.Test._Zip
{
    public class ZipStreamTests
    {
        [Fact]
        public void Test1()
        {
            using (var zip = new ZipStream())
            {
                zip.AddData("地地", "/哈哈.txt");
                zip.SaveAs("D:/123.zip");
            }
        }

    }
}
