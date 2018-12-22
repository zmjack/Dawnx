using Dawnx.Reflection;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dawnx.Linq
{
    public class WhereStringStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; private set; }

        public WhereStringStrategy(Expression<Func<TEntity, object>> inExp, Func<Expression, Expression, Expression> compareExp, string searchString)
        {
            if (!searchString.IsNullOrWhiteSpace())
                StrategyExpression = GenerateExpression(inExp, compareExp, searchString);
            else StrategyExpression = x => true;
        }

        private ParameterExpression[] GetParameters(Expression expression)
        {
            if (expression is null) return new ParameterExpression[0];

            if (expression is ParameterExpression)
                return new[] { expression as ParameterExpression };

            if (expression.NodeType == ExpressionType.Lambda)
                return Enumerable.ToArray(((dynamic)expression).Parameters);

            switch (expression)
            {
                case MemberExpression exp:
                    return GetParameters(exp.Expression);
                case UnaryExpression exp:
                    return GetParameters(exp.Operand);
                case MethodCallExpression exp:
                    return GetParameters(exp.Object)
                        .Concat(exp.Arguments.SelectMany(x => GetParameters(x)))
                        .ToArray();
                case NewExpression exp:
                    return exp.Arguments.SelectMany(x => GetParameters(x)).ToArray();

                default: return new ParameterExpression[0];
            }
        }

        private Expression GetReturnStringOrArrayExpression(Expression expression)
        {
            if (expression.Type == typeof(string))
                return expression;
            else if (expression.Type.GetInterface(typeof(IEnumerable).FullName) != null)
            {
                var ienumerableGenericType = new[] { expression.Type }
                    .Concat(expression.Type.GetInterfaces()).For(_ =>
                    {
                        var regex = new Regex(@"System\.Collections\.Generic\.IEnumerable`1\[(.+)\]");
                        foreach (var @interface in _)
                        {
                            var match = regex.Match(@interface.ToString());
                            if (match.Success)
                                return Type.GetType(match.Groups[1].Value);
                        }

                        throw new NotSupportedException("Only IEnumerable<T> is supported.");
                    });

                if (ienumerableGenericType != typeof(string))
                {
                    // If the T of IEnumerable<T> is not string,
                    // use System.Linq.Enumerable.Select method to convert it into string
                    var selectMethod = typeof(Enumerable)
                        .GetMethodViaFormatName("System.Collections.Generic.IEnumerable`1[TResult] Select[TSource,TResult](System.Collections.Generic.IEnumerable`1[TSource], System.Func`2[TSource,TResult])")
                        .MakeGenericMethod(ienumerableGenericType, typeof(string));

                    var parameter = Expression.Parameter(ienumerableGenericType);
                    var lambda = Expression.Lambda(
                        Expression.Call(parameter, typeof(object).GetMethod(nameof(object.ToString))), parameter);
                    return Expression.Call(selectMethod, expression, lambda);
                }
                else return expression;
            }
            else return Expression.Call(expression, typeof(object).GetMethod(nameof(object.ToString)));
        }

        private Expression<Func<TEntity, bool>> GenerateExpression(
            Expression<Func<TEntity, object>> inExp,
            Func<Expression, Expression, Expression> compareExp,
            string searchString)
        {
            Expression rightExp = Expression.Constant(searchString);

            switch (inExp.Body)
            {
                case NewExpression exp:
                    Expression leftExp = null;

                    foreach (var argExp in exp.Arguments)
                    {
                        if (leftExp is null)
                            leftExp = compareExp(GetReturnStringOrArrayExpression(argExp), rightExp);
                        else leftExp = Expression.OrElse(leftExp,
                            compareExp(GetReturnStringOrArrayExpression(argExp), rightExp));
                    }
                    return Expression.Lambda<Func<TEntity, bool>>(leftExp, inExp.Parameters);

                default:
                    return Expression.Lambda<Func<TEntity, bool>>(
                        compareExp(GetReturnStringOrArrayExpression(inExp.Body), rightExp), inExp.Parameters);
            }
        }

    }
}
