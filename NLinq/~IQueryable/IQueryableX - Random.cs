using Dawnx.Definition;
using NLinq.ProviderFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NLinq
{
    public static partial class IQueryableX
    {
        /// <summary>
        /// Select the specified number of random record from a source set.
        /// <para>[Warning] Before calling this function, you need to open the provider functions.</para>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Random<TSource>(this IQueryable<TSource> @this, int count)
            where TSource : class
        {
            var providerName = GetProviderName(@this);

            switch (providerName)
            {
                case DatabaseProviderName.Cosmos:
                case DatabaseProviderName.Firebird:
                case DatabaseProviderName.IBM:
                case DatabaseProviderName.Jet:
                case DatabaseProviderName.MyCat:
                case DatabaseProviderName.MySql: return @this.OrderBy(x => PMySql.Rand()).Take(count);
                case DatabaseProviderName.OpenEdge:
                case DatabaseProviderName.Oracle:
                case DatabaseProviderName.PostgreSQL:
                case DatabaseProviderName.Sqlite:
                case DatabaseProviderName.SqlServer:
                case DatabaseProviderName.SqlServerCompact35:
                case DatabaseProviderName.SqlServerCompact40:
                case DatabaseProviderName.Unknown:
                default:
                    throw new NotSupportedException("This method is not supported by the current provider.");
            }
        }

    }

}
