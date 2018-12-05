using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using Dawnx;
using Dawnx.Reflection;
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
