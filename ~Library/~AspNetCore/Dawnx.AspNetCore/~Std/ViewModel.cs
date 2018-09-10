using System;
using System.Linq.Expressions;
using System.Linq;

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

            //TODO: It will be optimized in the future, if this class is included in the standard library.
            var displayNameAttrType = exp.Member.GetCustomAttributes(false)
                .FirstOrDefault(x => x.GetType().FullName == "System.ComponentModel.DataAnnotations.DisplayAttribute");

            if (displayNameAttrType != null)
                return ((dynamic)displayNameAttrType).Name as string;
            else return exp.Member.Name;
        }

        public static string DisplayShortName<TRet>(Expression<Func<TModel, TRet>> expression)
        {
            var exp = expression.Body as MemberExpression;
            if (exp is null)
                throw new NotSupportedException("This argument 'expression' must be MemberExpression.");

            //TODO: It will be optimized in the future, if this class is included in the standard library.
            var displayNameAttrType = exp.Member.GetCustomAttributes(false)
                .FirstOrDefault(x => x.GetType().FullName == "System.ComponentModel.DataAnnotations.DisplayAttribute");

            if (displayNameAttrType != null)
                return ((dynamic)displayNameAttrType).Name as string;
            else return exp.Member.Name;
        }


    }
}
