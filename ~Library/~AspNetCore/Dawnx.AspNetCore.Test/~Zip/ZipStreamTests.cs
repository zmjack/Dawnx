﻿using System;
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
            //using (var zip = new ZipStream())
            //{
            //    zip.SetPassword("123")
            //        .AddEntry("english.txt", "this is a simple text".Bytes())
            //        .AddEntry("中文.txt", "这是一段简单文本".Bytes())
            //        .SaveAs("simple.zip");
            //}

            using (var zip = new ZipStream("simple.zip"))
            {
                zip.AddEntry("123/english1.txt", "this is a simple text".Bytes());
            }
        }

    }
}
