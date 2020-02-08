using Dawnx.Xml;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using Xunit;

namespace Dawnx.Test.Xml
{
    public class XmlTests
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

        [Fact]
        public void Test1()
        {
            var html = @"
<div id=""info"">hello</div>
<div class=""category1"">category_1</div>
<div class=""category2"">category_2</div>
<div id=""output"">bye</div>";

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var ctx = new MyContext("re");

            var sb = new StringBuilder();
            sb.AppendLine($"HTML:{html}");
            sb.AppendLine();
            // Samples
            var xpaths = new[]
            {
                @"//div[re:match(@class, 'category\d+')]",
                @"//div[re:match('(hello|bye)')]",
            };
            foreach (var xpath in xpaths)
            {
                sb.AppendLine($"XPath: {xpath}");
                var nodes = doc.DocumentNode.SelectNodes(ctx[xpath]);
                foreach (var node in nodes)
                    sb.AppendLine(node.InnerHtml);
                sb.AppendLine();
            }

            Assert.Equal(@"HTML:
<div id=""info"">hello</div>
<div class=""category1"">category_1</div>
<div class=""category2"">category_2</div>
<div id=""output"">bye</div>

XPath: //div[re:match(@class, 'category\d+')]
category_1
category_2

XPath: //div[re:match('(hello|bye)')]
hello
bye
", sb.ToString());
        }

    }
}
