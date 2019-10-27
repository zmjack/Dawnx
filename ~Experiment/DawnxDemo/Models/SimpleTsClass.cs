using TypeSharp;
using System;

namespace DawnxDemo.Models
{
    [TypeScriptModel]
    public class AAA
    {
        public Guid Id = Guid.Empty;
        public const string AAAA12 = "123";
        public EState State { get; set; }
        public string A { get; set; }
        public BBB B { get; set; }
        public DateTime DT { get; set; }
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
