﻿using System.Xml.XPath;
using System.Xml.Xsl;

namespace Dawnx.Xml
{
    internal class XPathVariable : IXsltContextVariable
    {
        public string Prefix { get; private set; }
        public string Name { get; private set; }

        public XPathVariable(string prefix, string name)
        {
            Prefix = prefix;
            Name = name;
        }

        public object Evaluate(XsltContext xsltContext)
        {
            var argList = ((XPathContext)xsltContext).ArgList;
            return argList.GetParam(Name, xsltContext.LookupNamespace(Prefix));
        }

        public bool IsLocal => false;
        public bool IsParam => true;
        public XPathResultType VariableType => XPathResultType.Any;

    }
}
