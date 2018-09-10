using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dawnx.AspNetCore
{
    public interface IViewModel<TSelf>
        where TSelf : class, IViewModel<TSelf>, new()
    { }

    public static class DawnIViewModel
    {
        public static string DisplayName<TModel, TRet>(this IEnumerable<IViewModel<TModel>> @this, Expression<Func<TModel, TRet>> expression)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.DisplayName(expression);
        public static string DisplayName<TModel, TRet>(this IViewModel<TModel> @this, Expression<Func<TModel, TRet>> expression)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.DisplayName(expression);

        public static string DisplayShortName<TModel, TRet>(this IEnumerable<IViewModel<TModel>> @this, Expression<Func<TModel, TRet>> expression)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.DisplayShortName(expression);
        public static string DisplayShortName<TModel, TRet>(this IViewModel<TModel> @this, Expression<Func<TModel, TRet>> expression)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.DisplayShortName(expression);

        public static string Display<TModel, TRet>(this IViewModel<TModel> @this, Expression<Func<TModel, TRet>> expression, string defaultReturn = "")
            where TModel : class, IViewModel<TModel>, new()
        {
            var exp = expression.Body as MemberExpression;
            if (exp is null)
                throw new NotSupportedException("This argument 'expression' must be MemberExpression.");

            TRet value;
            try
            {
                value = expression.Compile()(@this as TModel);
            }
            catch { value = default(TRet); }

            if (value != null)
            {
                dynamic valueValue;
                if (value is Nullable)
                    valueValue = ((dynamic)value).Value;
                else valueValue = value;

                //TODO: It will be optimized in the future, if this class is included in the standard library.
                var displayFormatAttrType = exp.Member
                    .GetCustomAttributes(false)
                    .FirstOrDefault(x => x.GetType().FullName == "System.ComponentModel.DataAnnotations.DisplayFormatAttribute");

                if (displayFormatAttrType != null)
                {
                    var attrValue_DataFormatString = ((dynamic)displayFormatAttrType).DataFormatString as string;

                    var ret = attrValue_DataFormatString.Replace("{0}", valueValue.ToString());
                    int startat = 0;
                    var regex = new Regex(@"\{0:(.+?)\}");
                    Match match;

                    while ((match = regex.Match(ret, startat)).Success)
                    {
                        var group = match.Groups[1];
                        var stringValue = valueValue.ToString(group.Value);
                        ret = ret.Replace($"{{0:{group.Value}}}", stringValue);
                        startat = group.Index - 3 + stringValue.Length;             // 3 = {0:
                    }
                    return ret;
                }
                else return value?.ToString();
            }
            else return defaultReturn;
        }

        public static void SetValue<TModel>(this IViewModel<TModel> @this, string propName, object value)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.InstanceType.GetProperty(propName).SetValue(@this, value);

        public static object GetValue<TModel>(this IViewModel<TModel> @this, string propName)
            where TModel : class, IViewModel<TModel>, new()
            => ViewModel<TModel>.InstanceType.GetProperty(propName).GetValue(@this);

    }
}
