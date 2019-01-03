using Dawnx.Ranges;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Dawnx.Utilities
{
    public static class StringUtility
    {
        /// <summary>
        /// Get the common starts of the specified strings.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string CommonStarts(params string[] strings)
        {
            if (strings.Length == 0) return string.Empty;
            else if (strings.Length == 1) return strings[0];
            else
            {
                var minLength = strings.Min(x => x.Length);
                var sb = new StringBuilder(minLength);

                for (int i = 0; i < minLength; i++)
                {
                    var take = strings[0][i];
                    if (strings.All(x => x[i] == take))
                        sb.Append(take);
                    else break;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Projects some strings back into an instance's field or property. (Using `?` on the right side of a variable disables greedy matching)
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="source"></param>
        /// <param name="instance"></param>
        /// <param name="patternExp"></param>
        public static void PatternMatch<TClass>(string source, TClass instance, Expression<Func<TClass, FormattableString>> patternExp)
            where TClass : class
        {
            var arguments = (patternExp.Body as MethodCallExpression)?.Arguments;
            if (arguments is null)
                throw new ArgumentException($"The argument `{nameof(patternExp)}` must be return a single line FormattableString.");

            var format = (arguments[0] as ConstantExpression).Value.ToString();
            var members = (arguments[1] as NewArrayExpression).Expressions.Select(exp =>
            {
                switch (exp)
                {
                    case MemberExpression memberExp: return memberExp.Member;
                    case UnaryExpression unaryExp: return (unaryExp.Operand as MemberExpression).Member;
                    default: throw new ArgumentException("At least one member info can't be accessed.");
                }
            }).ToArray();

            var prePattern = new[] { "\\", "/", "+", "*", "[", "]", "(", ")", "?", "|", "^" }
                .Aggregate(format, (_acc, ch) => _acc.Replace(ch, $"\\{ch}"));
            var pattern = new IntegerRange(0, members.Length - 1)
                .Aggregate(prePattern, (acc, i) => acc.Replace($"{{{i}}}\\?", @"(.+?)").Replace($"{{{i}}}", @"(.+)"));
            var regex = new Regex(pattern, RegexOptions.Singleline);

            var match = regex.Match(source);
            if (match.Success)
            {
                foreach (var i in new IntegerRange(1, members.Length))
                {
                    var value = match.Groups[i].Value;
                    switch (members[i - 1])
                    {
                        case FieldInfo member:
                            member.SetValue(instance, Convert.ChangeType(value, member.FieldType));
                            break;

                        case PropertyInfo member:
                            member.SetValue(instance, Convert.ChangeType(value, member.PropertyType));
                            break;

                        default:
                            throw new ArgumentException($"The access member must be {nameof(FieldInfo)} of {nameof(PropertyInfo)}.");
                    }
                }
            }
            else throw new ArgumentException($"The argument `{nameof(patternExp)}` can not match the specified string.");
        }

    }
}
