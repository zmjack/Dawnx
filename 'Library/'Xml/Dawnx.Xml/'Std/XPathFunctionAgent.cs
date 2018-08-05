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
            private XPathResultType[] _ArgTypes;

            public XPathFunctionAgent(string @namespace, string name, XPathResultType[] argTypes)
            {
                Namespace = @namespace;
                Name = name;
                _ArgTypes = argTypes;
            }

            public int Minargs => _ArgTypes.Length;
            public int Maxargs => _ArgTypes.Length;
            public XPathResultType ReturnType => XPathResultType.Any;
            public XPathResultType[] ArgTypes => _ArgTypes;

            public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
            {
                var context = xsltContext as XPathContext;
                context.Monitor?.Invoke(context, _ArgTypes, args, docContext);

                var funcDef = context.FunctionPool
                    .FirstOrDefault(func => func.Namespace == Namespace
                    && func.Name == Name
                    && string.Join(",", func.ArgTypes.Select(type => (int)type)) == string.Join(",", _ArgTypes.Select(type => (int)type)));

                if (funcDef != null)
                {
                    var methodParameterLength = funcDef.Method.GetParameters().Count();
                    var normalizedArgs = args.Select<object, object>((arg, i) =>
                    {
                        switch (_ArgTypes[i])
                        {
                            case XPathResultType.NodeSet:
                                return GetAttributeValue(args[i]);

                            default:
                                return args[i].ToString();
                        }
                    });

                    if (methodParameterLength == args.Length)
                    {
                        return funcDef.Method.Invoke(context, normalizedArgs.ToArray());
                    }
                    else if (methodParameterLength == args.Length + 1)
                    {
                        return funcDef.Method.Invoke(context,
                            normalizedArgs.Concat(new object[] { docContext }).ToArray());
                    }
                    else throw new ArgumentOutOfRangeException(
                        "The parameter length of the method must be equal to the 'args' length or 'args' length-1.");
                }
                else throw new KeyNotFoundException(
                    $"No function found. ({Namespace}::{Name}({string.Join(",", _ArgTypes)}))");
            }

            private string GetAttributeValue(dynamic arg)
            {
                // The type of dyArg is MS.Internal.Xml.XPath.XPathSelectionIterator.
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
