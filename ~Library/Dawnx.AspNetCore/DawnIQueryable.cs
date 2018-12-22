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

namespace Dawnx.AspNetCore
{
    public static class DawnIQueryable
    {
        private enum QueryCompilerVersion
        {
            Unknown, Version_2_0, Version_2_1
        }
        private static QueryCompilerVersion _QueryCompilerVersion = QueryCompilerVersion.Unknown;

        /// <summary>
        /// Gets the provider name of database.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetStorageName<TEntity>(this IQueryable<TEntity> @this)
            where TEntity : class
        {
            var queryCompiler = @this.Provider
                .GetFieldValue<EntityQueryProvider>("_queryCompiler") as QueryCompiler;

            var executionStrategyFactoryName = queryCompiler
                .GetFieldValue<QueryCompiler>("_queryContextFactory")
                .GetPropertyValue<RelationalQueryContextFactory>("ExecutionStrategyFactory")
                .ToString();

            switch (executionStrategyFactoryName)
            {
                case string name when name.Contains(Definition.DatabaseProvider.Cosmos): return Definition.DatabaseProvider.Cosmos;
                case string name when name.Contains(Definition.DatabaseProvider.Firebird): return Definition.DatabaseProvider.Firebird;
                case string name when name.Contains(Definition.DatabaseProvider.IBM): return Definition.DatabaseProvider.IBM;
                case string name when name.Contains(Definition.DatabaseProvider.Jet): return Definition.DatabaseProvider.Jet;
                case string name when name.Contains(Definition.DatabaseProvider.MyCat): return Definition.DatabaseProvider.MyCat;
                case string name when name.Contains(Definition.DatabaseProvider.MySql): return Definition.DatabaseProvider.MySql;
                case string name when name.Contains(Definition.DatabaseProvider.OpenEdge): return Definition.DatabaseProvider.OpenEdge;
                case string name when name.Contains(Definition.DatabaseProvider.Oracle): return Definition.DatabaseProvider.Oracle;
                case string name when name.Contains(Definition.DatabaseProvider.PostgreSQL): return Definition.DatabaseProvider.PostgreSQL;
                case string name when name.Contains(Definition.DatabaseProvider.Sqlite): return Definition.DatabaseProvider.Sqlite;
                case string name when name.Contains(Definition.DatabaseProvider.SqlServer): return Definition.DatabaseProvider.SqlServer;
                case string name when name.Contains(Definition.DatabaseProvider.SqlServerCompact35): return Definition.DatabaseProvider.SqlServerCompact35;
                case string name when name.Contains(Definition.DatabaseProvider.SqlServerCompact40): return Definition.DatabaseProvider.SqlServerCompact40;
                default: return null;
            }
        }

        /// <summary>
        /// Gets the generated Sql string.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToSql<TEntity>(this IQueryable<TEntity> @this)
            where TEntity : class
        {
            if (@this is EntityQueryable<TEntity>)
            {
                var queryCompiler = @this.Provider
                    .GetFieldValue<EntityQueryProvider>("_queryCompiler") as QueryCompiler;
                var dependencies = queryCompiler
                    .GetPropertyValue<QueryCompiler>("Database")
                    .GetPropertyValue<Database>("Dependencies") as DatabaseDependencies;

                var queryModel = queryCompiler.GetType().GetTypeInfo().DeclaredMembers.For(_ =>
                {
                    switch (_QueryCompilerVersion)
                    {
                        case QueryCompilerVersion.Unknown:
                            if (_.Any(x => x.Name == "_queryModelGenerator"))
                            {
                                _QueryCompilerVersion = QueryCompilerVersion.Version_2_1;
                                goto case QueryCompilerVersion.Version_2_1;
                            }
                            else if (_.Any(x => x.Name == "NodeTypeProvider"))
                            {
                                _QueryCompilerVersion = QueryCompilerVersion.Version_2_0;
                                goto case QueryCompilerVersion.Version_2_0;
                            }
                            else throw new NotSupportedException("Can not get QueryModel.");

                        case QueryCompilerVersion.Version_2_1:
                            var generator = queryCompiler
                                .GetFieldValue<QueryCompiler>("_queryModelGenerator");      // as QueryModelGenerator
                            return generator.InnerInvoke("ParseQuery", @this.Expression) as QueryModel;

                        case QueryCompilerVersion.Version_2_0:
                            var nodeTypeProvider = queryCompiler
                                .GetPropertyValue<QueryCompiler>("NodeTypeProvider") as INodeTypeProvider;
                            var parser = queryCompiler
                                .InnerInvoke<QueryCompiler>("CreateQueryParser", nodeTypeProvider) as QueryParser;
                            return parser.GetParsedQuery(@this.Expression);

                        default: throw new NotSupportedException();
                    }
                });

                var modelVisitor = dependencies.QueryCompilationContextFactory.Create(false)
                    .CreateQueryModelVisitor()
                    .Self(_ => _.CreateQueryExecutor<TEntity>(queryModel));

                return (modelVisitor as RelationalQueryModelVisitor)
                    .Queries.Select(x => $"{x.ToString().TrimEnd(';')};{Environment.NewLine}").Join("");
            }
            else return null;
        }

    }
}
