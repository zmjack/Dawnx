using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Dawnx.AspNetCore.Data
{
    public class WhereWrapper<TEntity>
        where TEntity : class
    {
        public DbContext DbContext { get; }
        public string TableName { get; }
        public string TableAlias { get; }
        public string WhereString { get; }
        public string ReferenceTag { get; }

        public WhereWrapper(DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> expression)
        {
            DbContext = dbSet.GetDbContext();

            var sql = dbSet.Where(expression).ToSql();
            var regex = new Regex(@"FROM\s+((.).+?[^\s])\s+AS\s+([^\s+|\r]+?)\s+WHERE\s+(.+)$", RegexOptions.Singleline);
            var match = regex.Match(sql);

            TableName = match.Groups[1].Value;
            ReferenceTag = match.Groups[2].Value;
            TableAlias = match.Groups[3].Value;
            WhereString = match.Groups[4].Value.Replace(TableAlias, TableName);
        }

        public UpdateWrapper<TEntity> Update() => new UpdateWrapper<TEntity>(this);
        public DeleteWrapper<TEntity> WrapDelete() => new DeleteWrapper<TEntity>(this);

        public int Update(Action<UpdateWrapper<TEntity>> setter)
        {
            var wrapper = Update();
            setter(wrapper);
            return DbContext.Database.ExecuteSqlCommand(wrapper.ToSql());
        }

        public int Delete()
        {
            var wrapper = WrapDelete();
            return DbContext.Database.ExecuteSqlCommand(wrapper.ToSql());
        }
    }

}
