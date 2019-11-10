using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Dawnx.Xml.SampleWithHAP
{
    class MyContext : XPathContext
    {
        public MyContext() { }
        public MyContext(string prefix) : base(prefix) { }

        [XPathFunction("match")]
        public bool RegexMatch1(string content, string regex)
        {
            Console.WriteLine($"  * Invoke: {nameof(RegexMatch1)}");
            return new Regex(regex).Match(content).Success;
        }

        [XPathFunction("match")]
        public bool RegexMatch2(string regex, XPathNavigator docContext)
        {
            Console.WriteLine($"  * Invoke: {nameof(RegexMatch2)}");
            return new Regex(regex).Match(docContext.InnerXml).Success;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var html = @"
<div id=""info"">hello</div>
<div class=""category1"">category_1</div>
<div class=""category2"">category_2</div>
<div id=""output"">bye</div>";

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var ctx = new MyContext("re");

            Console.WriteLine($"HTML:{html}");
            Console.WriteLine();

            // Samples
            var xpaths = new[]
            {
            @"//div[re:match(@class, 'category\d+')]",
            @"//div[re:match('(hello|bye)')]",
        };
            foreach (var xpath in xpaths)
            {
                Console.WriteLine($"XPath: {xpath}");
                var nodes = doc.DocumentNode.SelectNodes(ctx[xpath]);
                foreach (var node in nodes)
                    Console.WriteLine(node.InnerHtml);
                Console.WriteLine();
            }
        }
    }
}
