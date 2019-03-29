using Dawnx.Annotation;

namespace DawnxDevelopingWeb.Models
{
    [TsGen]
    public class AAA
    {
        public const string AAAA = "123";
        public EState State { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }

    [TsGen]
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
