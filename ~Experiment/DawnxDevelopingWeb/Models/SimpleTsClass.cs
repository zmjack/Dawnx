using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLite;

namespace DawnxDevelopingWeb.Models
{
    [TsClass]
    public class AAA
    {
        public const string AAAA = "123";
        public EState State { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }

    [TsClass]
    public class BBB
    {
        public EState State { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }

    public enum EState
    {
        Ready, Running, Complete
    }

}
