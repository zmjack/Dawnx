using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Dawnx.Xml.Test
{
    class MyContext : XPathContext
    {
        public override string DefaultNamespace => "http://uri";

        [XPathFunction("match", XPathResultType.NodeSet, XPathResultType.String)]
        public bool RegexMatch(string prop, string regex, XPathNavigator docContext)
        {
            return new Regex(regex).Match(prop).Success;
        }
    }

}
