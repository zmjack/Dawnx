using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Dawnx.Xml
{
    public partial class XPathContext
    {
        internal class XPathFunctionAgent : IXsltContextFunction
        {
            private string Namespace;
            private string Name;

            public XPathFunctionAgent(string @namespace, string name)
            {
                Namespace = @namespace;
                Name = name;
            }

            public int Minargs => throw new NotSupportedException();
            public int Maxargs => throw new NotSupportedException();
            public XPathResultType ReturnType => XPathResultType.Any;
            public XPathResultType[] ArgTypes => throw new NotSupportedException();

            public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
            {
                var context = xsltContext as XPathContext;
                var argTypes = args.Select(x =>
                {
                    switch (x.GetType().FullName)
                    {
                        case "MS.Internal.Xml.XPath.XPathSelectionIterator": return typeof(string);
                        default: return x.GetType();
                    }
                });
                var customFunc = context.CustomFunctions
                    .FirstOrDefault(x => x.Namespace == Namespace && x.Name == Name
                                      && Enumerable.SequenceEqual(argTypes, x.ArgTypes));

                if (customFunc != null)
                {
                    var methodParameterLength = customFunc.Method.GetParameters().Count();
                    var funcArgs = args.Select<object, object>((arg, i) =>
                    {
                        switch (arg.GetType().FullName)
                        {
                            case "MS.Internal.Xml.XPath.XPathSelectionIterator": return GetAttributeValue(args[i]);
                            default: return args[i].ToString();
                        }
                    });

                    if (methodParameterLength == args.Length)
                    {
                        return customFunc.Method.Invoke(context, funcArgs.ToArray());
                    }
                    else if (methodParameterLength == args.Length + 1)
                    {
                        return customFunc.Method.Invoke(context,
                            funcArgs.Concat(new object[] { docContext }).ToArray());
                    }
                    else throw new ArgumentOutOfRangeException(
                        "The parameter length of the method must be equal to the 'args' length or 'args' length-1.");
                }
                else throw new KeyNotFoundException(
                    $"No function found. ({Namespace}::{Name}({string.Join(",", ArgTypes)}))");
            }

            private string GetAttributeValue(dynamic arg)
            {
                // The type of arg is MS.Internal.Xml.XPath.XPathSelectionIterator.
                // It isn't a public type. So, use dynamic instead.
                XPathNodeType nodeType = arg.Current.NodeType;

                switch (nodeType)
                {
                    case XPathNodeType.Element:
                        foreach (dynamic _arg in arg)
                            return _arg.InnerXml;
                        goto default;

                    default:
                        return string.Empty;
                }
            }
        }

    }
}
