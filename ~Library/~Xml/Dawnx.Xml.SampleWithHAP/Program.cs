using HtmlAgilityPack;
using System;
using System.Web;

namespace Dawnx.Xml.SampleWithHAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlDocument().Self(_ =>
            {
                _.LoadHtml(
                    @"<div id=""info"">hello</div>" +
                    @"<div id=""category_1"">category_1</div>" +
                    @"<div id=""category_2"">category_2</div>" +
                    @"<div id=""output"">bye</div>");
            });
            
            var context = new MyContext().Self(_ =>
            {
                _.AddNamespace("fn");
            });

            // Sample 1: Use a regex match to specify a string
            Console.WriteLine("Sample 1: Use a regex match to specify a string");
            doc.DocumentNode.SelectNodes(
                context.Compile(@"//div[fn:match(@id, 'category_\d+')]")).Each(node =>
                {
                    Console.WriteLine(HttpUtility.HtmlDecode(node.InnerHtml));
                });

            Console.WriteLine();

            // Sample 2: Use a regex match to InnerXml
            Console.WriteLine("Sample 2: Use a regex match to InnerXml");
            doc.DocumentNode.SelectNodes(
                context.Compile(@"//div[fn:match('(hello|bye)')]")).Each(node =>
                {
                    Console.WriteLine(HttpUtility.HtmlDecode(node.InnerHtml));
                });
        }
    }
}
