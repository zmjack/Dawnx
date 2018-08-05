using HtmlAgilityPack;
using System.Collections.Generic;
using Xunit;

namespace Dawnx.Xml.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = new List<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(@"<div id=""info"">info</div>
<div id=""category_1"">category_1</div>
<div id=""category_2"">category_2</div>
<div id=""output"">output</div>");

            var context = new MyContext();
            context.AddNamespace("fn", context.DefaultNamespace);
            context.AddParam("regex", context.DefaultNamespace, @"category_\d+");

            var xpath = context.Compile(@"//div[fn:match(@id, $fn:regex)]");
            var nodes = doc.DocumentNode.SelectNodes(xpath);
            nodes.Each(node => result.Add(node.InnerHtml));

            Assert.Equal("category_1", result[0]);
            Assert.Equal("category_2", result[1]);
        }
    }
}
