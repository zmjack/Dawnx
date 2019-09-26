using TypeSharp;

namespace TypeSharp.Test
{
    [TypeScriptModel(Namespace = "TSNS1")]
    public class RootClass
    {
        public const string CONST_STRING = "const_string";
        public const int CONST_INTEGER = int.MaxValue;
        public EState State { get; set; }
        public SubClass BBB { get; set; }
        public string Str { get; set; }
        public int Int { get; set; }
    }

    [TypeScriptModel(Namespace = "TSNS2")]
    public class SubClass
    {
        public string Name { get; set; }
    }

    public enum EState
    {
        Ready, Running, Complete
    }
}
