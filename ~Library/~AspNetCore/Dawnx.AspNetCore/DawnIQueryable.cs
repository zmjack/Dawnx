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
    public static class DawnIQueryable
    {
        private enum QueryCompilerVersion
        {
            Unknown, Version_2_0, Version_2_1
        }
        private static QueryCompilerVersion _QueryCompilerVersion = QueryCompilerVersion.Unknown;

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
