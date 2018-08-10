using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Dawnx.Xml.SampleWithHAP
{
    class MyContext : XPathContext
    {
        public override string DefaultNamespace => "";

        // Use a regex match to specify a string
        [XPathFunction("match", XPathResultType.NodeSet, XPathResultType.String)]
        public bool RegexMatch(string prop, string regex)
        {
            return new Regex(regex).Match(prop).Success;
        }

        // Use a regex match to InnerXml
        [XPathFunction("match", XPathResultType.String)]
        public bool RegexMatch(string regex, XPathNavigator docContext)
        {
            return new Regex(regex).Match(docContext.InnerXml).Success;
        }

    }
}