using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    internal class ExactEqualityComparer<T> : IEqualityComparer<T>
    {
        private Expression<Func<T, object>> _expression;

        public ExactEqualityComparer(Expression<Func<T, object>> expression)
        {
            _expression = expression;
        }

        public bool Equals(T v1, T v2)
        {
            string[] propNames;
            switch (_expression.Body)
            {
                case MemberExpression exp:
                    propNames = new[] { exp.Member.Name };
                    break;

                case NewExpression exp:
                    propNames = exp.Members.Select(x => x.Name).ToArray();
                    break;

                default:
                    throw new NotSupportedException("This argument 'includes' must be MemberExpression or NewExpression.");
            }

            foreach (var propName in propNames)
            {
                var obj1 = _expression.Compile()(v1);
                var obj2 = _expression.Compile()(v2);

                if (!obj1.Equals(obj2))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj) => 0;
    }

}