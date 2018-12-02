using Dawnx;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sapling
{
    public static class SpUploadBox
    {
        public class Config : ISaplingConfig
        {
            public string StatUrl { get; set; }
            public string UploadUrl { get; set; }
        }
    }
}
