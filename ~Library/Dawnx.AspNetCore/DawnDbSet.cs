using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore
{
    public static class DawnDbSet
    {
        public static string ToSql<TEntity>(this DbSet<TEntity> @this)
            where TEntity : class
            => DawnIQueryable.ToSql(@this.Where(x => true));
    }
}
