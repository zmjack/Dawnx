using System;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dawnx.AspNetCore
{
    public static class ViewModel<TModel>
        where TModel : new()
    {
        public static Type InstanceType = new TModel().GetType();

        public static string DisplayName<TRet>(Expression<Func<TModel, TRet>> expression)
        {
            var exp = expression.Body as MemberExpression;
            if (exp is null)
                throw new NotSupportedException("This argument 'expression' must be MemberExpression.");

            return NetCompatibility.GetDisplayNameFromAttribute(exp.Member);
        }

        public static string DisplayShortName<TRet>(Expression<Func<TModel, TRet>> expression)
        {
            var exp = expression.Body as MemberExpression;
            if (exp is null)
                throw new NotSupportedException("This argument 'expression' must be MemberExpression.");

            return exp.Member.GetCustomAttribute<DisplayAttribute>()?.ShortName ?? exp.Member.Name;
        }

    }
}
