using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dawnx.Utilities
{
    public static class ExpressionUtility
    {
        public static string[] GetPropertyNames<TEntity>(Expression<Func<TEntity, object>> memberOrNewExp)
        {
            string[] propNames;
            switch (memberOrNewExp.Body)
            {
                case MemberExpression exp:
                    propNames = new[] { exp.Member.Name };
                    break;

                case NewExpression exp:
                    propNames = exp.Members.Select(x => x.Name).ToArray();
                    break;

                default:
                    throw new NotSupportedException("This argument must be MemberExpression or NewExpression.");
            }

            return propNames;
        }

        public static IEnumerable<PropertyInfo> GetProperties<TEntity>(Expression<Func<TEntity, object>> memberOrNewExp)
        {
            var propNames = GetPropertyNames(memberOrNewExp);
            var type = typeof(TEntity);
            var props = type.GetProperties().Where(x => propNames.Contains(x.Name));
            return props;
        }

        public static MemberExpression[] GetSingleExpressions<TEntity>(Expression<Func<TEntity, object>> memberOrNewExp)
        {
            var propNames = GetPropertyNames(memberOrNewExp);
            return propNames.Select(name => Expression.PropertyOrField(Expression.Parameter(typeof(TEntity)), name)).ToArray();
        }

    }
}
