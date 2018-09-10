using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dawnx
{
    public class WhereStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        private readonly string _searchString;
        private Expression<Func<TEntity, object>> _expression;
        private Func<Expression, Expression, Expression> _binaryGenerator;

        public Expression<Func<TEntity, bool>> StrategyExpression { get; private set; }

        public WhereStrategy(Func<Expression, Expression, Expression> binaryGenerator, string searchString, Expression<Func<TEntity, object>> expression)
        {
            if (!searchString.IsNullOrWhiteSpace())
            {
                _binaryGenerator = binaryGenerator;
                _searchString = searchString;
                _expression = expression;
                StrategyExpression = GenerateExpression();
            }
            else StrategyExpression = x => true;
        }

        private ParameterExpression[] GetParameterExpressions(Expression expression)
        {
            if (expression is null) return new ParameterExpression[0];

            if (expression is ParameterExpression)
                return new[] { expression as ParameterExpression };

            if (expression.NodeType == ExpressionType.Lambda)
                return Enumerable.ToArray(((dynamic)expression).Parameters);

            switch (expression)
            {
                case MemberExpression exp:
                    return GetParameterExpressions(exp.Expression);
                case UnaryExpression exp:
                    return GetParameterExpressions(exp.Operand);
                case MethodCallExpression exp:
                    return GetParameterExpressions(exp.Object)
                        .Concat(exp.Arguments.SelectMany(x => GetParameterExpressions(x)))
                        .ToArray();
                case NewExpression exp:
                    return exp.Arguments.SelectMany(x => GetParameterExpressions(x)).ToArray();

                default: return new ParameterExpression[0];
            }
        }

        private Expression GetReturnStringOrArrayExpression(Expression expression)
        {
            if (expression.Type == typeof(string))
                return expression;
            else if (expression.Type.GetInterface(typeof(IEnumerable).FullName) != null)
            {
                var ienumerableGenericType = new[] { expression.Type }.Concat(expression.Type.GetInterfaces()).For(_ =>
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
                    // If the T of IEnumerable<T> is not string, use System.Linq.Enumerable.Select method to convert it into string
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

        private Expression<Func<TEntity, bool>> GenerateExpression()
        {
            Expression rightExp = Expression.Constant(_searchString);

            switch (_expression.Body)
            {
                case NewExpression exp:
                    Expression leftExp = null;

                    foreach (var argExp in exp.Arguments)
                    {
                        if (leftExp is null)
                            leftExp = _binaryGenerator(GetReturnStringOrArrayExpression(argExp), rightExp);
                        else leftExp = Expression.OrElse(leftExp,
                            _binaryGenerator(GetReturnStringOrArrayExpression(argExp), rightExp));
                    }
                    return Expression.Lambda<Func<TEntity, bool>>(leftExp, _expression.Parameters);

                default:
                    return Expression.Lambda<Func<TEntity, bool>>
                        (_binaryGenerator(GetReturnStringOrArrayExpression(_expression.Body), rightExp), _expression.Parameters);
            }
        }

    }
}
