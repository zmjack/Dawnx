using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Dawnx
{
    public interface IEntity<TSelf> : IEntity
        where TSelf : class, IEntity<TSelf>, new()
    { }

    public static class DawnIEntity_T
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
            => Display<TEntity>(@this, expression as LambdaExpression, defaultReturn);
        public static string Display<TEntity>(this IEntity<TEntity> @this, LambdaExpression expression, string defaultReturn = "")
            where TEntity : class, IEntity<TEntity>, new()
        {
            var exp = expression.Body as MemberExpression;
            if (exp is null)
                throw new NotSupportedException("This argument 'expression' must be MemberExpression.");

            object value;
            try
            {
                value = expression.Compile().DynamicInvoke(new object[] { @this as TEntity });
            }
            catch { value = null; }

            if (value != null)
            {
                dynamic dValue = value is Nullable ? ((dynamic)value).Value : value;

                var displayFormatAttrType = exp.Member.GetCustomAttributes(false)
                    .FirstOrDefault(x => x is DisplayFormatAttribute) as DisplayFormatAttribute;

                if (displayFormatAttrType != null)
                {
                    var attrValue_DataFormatString = displayFormatAttrType.DataFormatString as string;

                    var ret = attrValue_DataFormatString.Replace("{0}", dValue.ToString());
                    int startat = 0;
                    var regex = new Regex(@"\{0:(.+?)\}");
                    Match match;

                    while ((match = regex.Match(ret, startat)).Success)
                    {
                        var group = match.Groups[1];
                        var stringValue = dValue.ToString(group.Value);
                        ret = ret.Replace($"{{0:{group.Value}}}", stringValue);
                        startat = group.Index - 3 + stringValue.Length;             // 3 = {0:
                    }
                    return ret;
                }
                else
                {
                    if (value.GetType().BaseType.FullName == "System.Enum")
                        return DataAnnotationUtility.GetDisplayName(value.GetType().GetFields().First(x => x.Name == value.ToString()));
                    else return value.ToString();
                }
            }
            else return defaultReturn;
        }

        public static void SetValue<TEntity>(this IEntity<TEntity> @this, string propName, object value)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.InstanceType.GetProperty(propName).SetValue(@this, value);

        public static object GetValue<TEntity>(this IEntity<TEntity> @this, string propName)
            where TEntity : class, IEntity<TEntity>, new()
            => ViewModel<TEntity>.InstanceType.GetProperty(propName).GetValue(@this);

    }
}
