using System;
using System.Xml.XPath;

namespace Dawnx.Xml
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class XPathFunctionAttribute : Attribute
    {
        public string Namespace { get; private set; }
        public string Name { get; private set; }
        public XPathResultType[] ArgTypes { get; private set; }

        /// <summary>
        /// Defines a function named '{DefaultNamespace}:{FunctionName}' in the context.
        ///     Project each <see cref="XPathResultType"/> into the function's arguments.
        ///     (If you need the 'docContext', you must use a <see cref="XPathNavigator"/> parameter to receive it.)
        /// </summary>
        /// <param name="argTypes"></param>
        public XPathFunctionAttribute(params XPathResultType[] argTypes) : this(null, null, argTypes) { }

        /// <summary>
        /// Defines a function named '{DefaultNamespace}:{$name}' in the context.
        ///     Project each <see cref="XPathResultType"/> into the function's arguments.
        ///     (If you need the 'docContext', you must use a <see cref="XPathNavigator"/> parameter to receive it.)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="argTypes"></param>
        public XPathFunctionAttribute(string name, params XPathResultType[] argTypes) : this(null, name, argTypes) { }

        /// <summary>
        /// Defines a function named '{$namespace}:{$name}' in the context.
        ///     Project each <see cref="XPathResultType"/> into the function's arguments.
        ///     (If you need the 'docContext', you must use a <see cref="XPathNavigator"/> parameter to receive it.)
        /// </summary>
        /// <param name="namespace"></param>
        /// <param name="name"></param>
        /// <param name="argTypes"></param>
        public XPathFunctionAttribute(string @namespace, string name, params XPathResultType[] argTypes)
        {
            Namespace = @namespace;
            Name = name;
            ArgTypes = argTypes;
        }
    }
}