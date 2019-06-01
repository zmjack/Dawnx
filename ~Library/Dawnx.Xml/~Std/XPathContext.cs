using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Dawnx.Xml
{
    public abstract partial class XPathContext : XsltContext
    {
        private class ContextFunction
        {
            public string Namespace { get; set; }
            public string Name { get; set; }
            public XPathResultType[] ArgTypes { get; set; }
            public MethodInfo Method { get; set; }
        }

        /// <summary>
        /// This event has the source arguments that are available before the defined function is called.
        ///     And it is called when the function defined in the context is called.
        /// </summary>
        public event OnMonitor Monitor;
        public delegate void OnMonitor(XPathContext sender, XPathResultType[] argTypes, object[] args, XPathNavigator docContext);

        private HashSet<ContextFunction> FunctionPool = new HashSet<ContextFunction>();

        public XPathContext()
        {
            var contextMethods = GetType().GetMethods()
                .Where(method => method.GetCustomAttribute<XPathFunctionAttribute>() != null);

            foreach (var method in contextMethods)
            {
                var attrs = method.GetCustomAttributes<XPathFunctionAttribute>();
                foreach (var attr in attrs)
                {
                    FunctionPool.Add(new ContextFunction
                    {
                        Namespace = attr.Namespace ?? DefaultNamespace,
                        Name = attr.Name ?? method.Name,
                        ArgTypes = attr.ArgTypes,
                        Method = method,
                    });
                }
            }
        }

        /// <summary>
        /// Gets all the defined argumennts in the context.
        /// </summary>
        public XsltArgumentList ArgList { get; private set; } = new XsltArgumentList();

        /// <summary>
        /// Evaluates whether to preserve white space nodes or strip them for the given context.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override bool PreserveWhitespace(XPathNavigator node) => false;

        /// <summary>
        /// Compares the base Uniform Resource Identifiers
        ///     (URIs) of two documents based upon the order the documents were loaded by the
        ///     XSLT processor (that is, the System.Xml.Xsl.XslTransform class)
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="nextbaseUri"></param>
        /// <returns></returns>
        public override int CompareDocument(string baseUri, string nextbaseUri) => 0;

        /// <summary>
        /// Gets a value indicating whether to include white space nodes in the output.
        /// </summary>
        public override bool Whitespace => true;

        /// <summary>
        /// Resolves a function reference and returns
        ///     an <see cref="IXsltContextFunction"/> representing the function. The <see cref="IXsltContextFunction"/> 
        ///     is used at execution time to get the return value of the function.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] argTypes)
            => new XPathFunctionAgent(LookupNamespace(prefix), name, argTypes);

        /// <summary>
        /// Resolves a variable reference and returns
        ///     an System.Xml.Xsl.IXsltContextVariable representing the variable.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IXsltContextVariable ResolveVariable(string prefix, string name)
            => new XPathVariable(prefix, name);

        /// <summary>
        /// Adds the given namespace to the collection for 'DefaultNamespace'.
        /// </summary>
        /// <param name="prefix"></param>
        public void AddNamespace(string prefix) => AddNamespace(prefix, DefaultNamespace);

        /// <summary>
        /// Adds a argument to <see cref="ArgList"/> and associates it with the namespace qualified name. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="namespaceUri"></param>
        /// <param name="parameter"></param>
        public void AddParam(string name, string namespaceUri, object parameter)
            => ArgList.AddParam(name, namespaceUri, parameter);

        /// <summary>
        /// Adds a argument to <see cref="ArgList"/> and associates it with empty namespace.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        public void AddParam(string name, object parameter)
            => ArgList.AddParam(name, "", parameter);

        /// <summary>
        /// Compiles the XPath expression specified and returns an System.Xml.XPath.XPathExpression
        ///     object representing the XPath expression.
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public XPathExpression Compile(string xpath)
        {
            var xExp = XPathExpression.Compile(xpath);
            xExp.SetContext(this);
            return xExp;
        }

    }
}
