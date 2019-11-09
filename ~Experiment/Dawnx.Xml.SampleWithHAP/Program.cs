using HtmlAgilityPack;
using NStandard;
using System;
using System.Web;

namespace Dawnx.Xml.SampleWithHAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = @"
<div id=""info"">hello</div>
<div id=""category_1"">category_1</div>
<div id=""category_2"">category_2</div>
<div id=""output"">bye</div>";

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var context = new MyContext();
            context.AddNamespace("fn");

            Console.WriteLine($"Simple HTML:{html}");
            Console.WriteLine();

            // Samples
            HtmlNodeCollection nodes;

            // Sample 1: Use a regex match to specify a string
            Console.WriteLine("Sample 1: Use a regex match to specify a string");
            nodes = doc.DocumentNode.SelectNodes(
                context.Compile(@"//div[fn:match(@id, 'category_\d+')]"));
            foreach (var node in nodes)
                Console.WriteLine(node.InnerHtml);

            Console.WriteLine();

            // Sample 2: Use a regex match to InnerXml
            Console.WriteLine("Sample 2: Use a regex match to InnerXml");
            nodes = doc.DocumentNode.SelectNodes(
                context.Compile(@"//div[fn:match('(hello|bye)')]"));
            foreach (var node in nodes)
                Console.WriteLine(node.InnerHtml);
        }
    }
}
