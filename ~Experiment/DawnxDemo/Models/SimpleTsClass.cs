using Dawnx.Annotation;
using System;

namespace DawnxDemo.Models
{
    [TypeScriptModel]
    public class AAA
    {
        public const string AAAA12 = "123";
        public EState State { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public BBB DT { get; set; }
    }

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
