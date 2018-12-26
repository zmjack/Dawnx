using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dawnx.AspNetCore.Data
{
    public class UpdateWrapper<TEntity>
        where TEntity : class
    {
        public WhereWrapper<TEntity> WhereWrapper { get; }

        public Dictionary<string, object> FieldChanges = new Dictionary<string, object>();

        public UpdateWrapper(WhereWrapper<TEntity> whereWrapper)
        {
            WhereWrapper = whereWrapper;
        }

        public UpdateWrapper<TEntity> Set<TRet>(Expression<Func<TEntity, TRet>> expression, TRet value)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var body = (expression.Body as MemberExpression).Member;
                FieldChanges.Add(body.GetCustomAttribute<ColumnAttribute>()?.Name ?? body.Name,
                    $"{WhereWrapper.ReferenceTag}{value.ToString()}{WhereWrapper.ReferenceTag}");
            }
            return this;
        }

        public string ToSql()
        {
            if (!FieldChanges.Any())
                throw new ArgumentException("The `set` statement is null.");

            var set = FieldChanges.Select(x => $"{WhereWrapper.ReferenceTag}{x.Key}{WhereWrapper.ReferenceTag}={x.Value}").Join(",");
            return $"UPDATE {WhereWrapper.TableName} SET {set} WHERE {WhereWrapper.WhereString}";
        }

        public int Save() => WhereWrapper.DbContext.Database.ExecuteSqlCommand(ToSql());
    }
}
