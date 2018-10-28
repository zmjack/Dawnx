using Dawnx.Entity;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dawnx
{
    public static partial class DawnIEntity
    {
        public static string DisplayName<TEntity, TRet>(this IEnumerable<IEntity<TEntity>> @this, Expression<Func<TEntity, TRet>> expression)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.DisplayName(expression);
        public static string DisplayName<TEntity, TRet>(this IEntity<TEntity> @this, Expression<Func<TEntity, TRet>> expression)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.DisplayName(expression);

        public static string DisplayShortName<TEntity, TRet>(this IEnumerable<IEntity<TEntity>> @this, Expression<Func<TEntity, TRet>> expression)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.DisplayShortName(expression);
        public static string DisplayShortName<TEntity, TRet>(this IEntity<TEntity> @this, Expression<Func<TEntity, TRet>> expression)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.DisplayShortName(expression);

        public static string Display<TEntity, TRet>(this IEntity<TEntity> @this, Expression<Func<TEntity, TRet>> expression, string defaultReturn = "")
            where TEntity : class, IEntity<TEntity>, new()
            => DataAnnotationUtility.GetDisplayString(@this, expression, defaultReturn);

        public static Dictionary<string, string> ToDisplayDictionary<TEntity>(this IEntity<TEntity> @this, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity<TEntity>, new()
        {
            string[] propNames;
            switch (includes.Body)
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

            return ToDisplayDictionary(@this as IEntity, propNames);
        }

    }
}
