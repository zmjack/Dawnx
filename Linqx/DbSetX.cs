using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Linqx
{
    public static class DbSetX
    {
        public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> @this)
            where TEntity : class
        {
            var provider = (@this as IInfrastructure<IServiceProvider>).Instance;
            return (provider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext).Context;
        }

        public static UpdateWrapper<TEntity> TryUpdate<TEntity>(this DbSet<TEntity> @this, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
            => new UpdateWrapper<TEntity>(new WhereWrapper<TEntity>(@this, expression));

        public static DeleteWrapper<TEntity> TryDelete<TEntity>(this DbSet<TEntity> @this, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
            => new DeleteWrapper<TEntity>(new WhereWrapper<TEntity>(@this, expression));

        public static string ToSql<TEntity>(this DbSet<TEntity> @this)
            where TEntity : class
            => IQueryableX.ToSql(@this.Where(x => true));

    }
}
