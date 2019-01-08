using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
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
        public string ReferenceTagA { get; }
        public string ReferenceTagB { get; }

        public WhereWrapper(DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> expression)
        {
            DbContext = dbSet.GetDbContext();

            var sql = dbSet.Where(expression).ToSql();
            var regex = new Regex(@"FROM\s+((.).+?(.))\s+AS\s+([^\s+|\r]+?)\s+WHERE\s+(.+)$", RegexOptions.Singleline);
            var match = regex.Match(sql);

            TableName = match.Groups[1].Value;
            ReferenceTagA = match.Groups[2].Value;
            ReferenceTagB = match.Groups[3].Value;
            TableAlias = match.Groups[4].Value;
            WhereString = match.Groups[5].Value.Replace(TableAlias, TableName);
        }
    
    }

}
