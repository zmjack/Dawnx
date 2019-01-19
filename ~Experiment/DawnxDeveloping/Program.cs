#if !USE
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Dawnx.Algorithms.StringAlgorithm;
using SimpleData;
using Dawnx;
using Dawnx.AspNetCore;
using System.Linq.Expressions;
using SimpleData.Northwnd;
using Dawnx.Net.Web;
using Dawnx.Net.Web.Processors;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace DawnxDevloping
{
    class Program
    {
        static IEnumerable<HtmlNode> GetPureNodes(IEnumerable<HtmlNode> nodes)
        {
            foreach (var node in nodes)
            {
                switch (node.Name)
                {
                    case "#text": break;
                    case "ol":
                        foreach (var item in GetPureNodes(node.ChildNodes))
                            yield return item;
                        break;

                    default: yield return node; break;
                }
            }
        }

        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(@"
<h2 id=""Title1"">1-2</h2>
<h3 id=""Title1"">1-2-1</h3>
<ol>
<li></li>
</ol>");
            var pureNodes = GetPureNodes(doc.DocumentNode.ChildNodes).ToArray();
        }

    }
}
#endif
