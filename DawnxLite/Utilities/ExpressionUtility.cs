using System.Linq.Expressions;

namespace Dawnx.Utilities
{
    public static class ExpressionUtility
    {
        public class RewriteVisitor : ExpressionVisitor
        {
            private readonly Expression _from, _to;
            public RewriteVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }
            public override Expression Visit(Expression node) => node == _from ? _to : base.Visit(node);
        }

        public static Expression RebindParameter(Expression expression, ParameterExpression origin, ParameterExpression target)
        {
            return new RewriteVisitor(origin, target).Visit(expression);
        }
    }
}
