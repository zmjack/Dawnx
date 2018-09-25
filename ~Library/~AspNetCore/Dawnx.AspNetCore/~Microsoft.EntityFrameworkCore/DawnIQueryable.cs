using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using Dawnx;
using Dawnx.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    public static class IQueryableExtensions
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> @this)
        {
            if (@this is EntityQueryable<TEntity>)
            {
                var queryCompiler = @this.Provider
                    .GetFieldValue<EntityQueryProvider, QueryCompiler>("_queryCompiler");
                var database = queryCompiler
                    .GetPropertyValue<QueryCompiler, Database>("Database");
                var dependencies = database
                    .GetPropertyValue<Database, DatabaseDependencies>("Dependencies");

                var nodeTypeProvider = queryCompiler
                    .GetPropertyValue<QueryCompiler, INodeTypeProvider>("NodeTypeProvider");

                var parser = queryCompiler
                    .InnerInvoke<QueryCompiler, QueryParser>("CreateQueryParser", nodeTypeProvider);

                var modelVisitor = dependencies.QueryCompilationContextFactory.Create(false)
                    .CreateQueryModelVisitor()
                    .Self(_ => _.CreateQueryExecutor<TEntity>(parser.GetParsedQuery(@this.Expression)));

                return (modelVisitor as RelationalQueryModelVisitor)
                    .Queries.Select(x => $"{x.ToString().TrimEnd(';')};{Environment.NewLine}").Join("");
            }
            else return null;
        }

    }
}
