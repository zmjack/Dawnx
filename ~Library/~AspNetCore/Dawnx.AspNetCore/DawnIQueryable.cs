using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Linq;
using System.Reflection;

namespace Dawnx.AspNetCore
{
    public static class DawnIQueryable
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> @this)
        {
            if (@this is EntityQueryable<TEntity>)
            {
                var queryCompiler = typeof(EntityQueryProvider).GetTypeInfo()
                    .GetDeclaredField("_queryCompiler")
                    .GetValue(@this.Provider) as QueryCompiler;
                var database = typeof(QueryCompiler).GetTypeInfo()
                    .GetDeclaredProperty("Database")
                    .GetValue(queryCompiler) as Database;
                var dependencies = typeof(Database).GetTypeInfo()
                    .GetDeclaredProperty("Dependencies")
                    .GetValue(database) as DatabaseDependencies;

                var queryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();
                var nodeTypeProvider = queryCompilerTypeInfo
                    .GetDeclaredProperty("NodeTypeProvider")
                    .GetValue(queryCompiler) as INodeTypeProvider;
                var parser = queryCompilerTypeInfo
                    .GetDeclaredMethod("CreateQueryParser")
                    .Invoke(queryCompiler, new object[] { nodeTypeProvider }) as QueryParser;

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
