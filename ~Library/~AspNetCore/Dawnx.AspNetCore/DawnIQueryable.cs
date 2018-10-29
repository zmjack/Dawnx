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
        public static string ToSql<TEntity>(this IQueryable<TEntity> @this)
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
                    //TODO: Maybe use cache to optimize

                    // Core 2.1
                    if (_.Any(x => x.Name == "_queryModelGenerator"))
                    {
                        var generator = queryCompiler
                            .GetFieldValue<QueryCompiler>("_queryModelGenerator");      // as QueryModelGenerator
                        return generator.InnerInvoke("ParseQuery", @this.Expression) as QueryModel;
                    }

                    // Core 2.0
                    if (_.Any(x => x.Name == "NodeTypeProvider"))
                    {
                        var nodeTypeProvider = queryCompiler
                            .GetPropertyValue<QueryCompiler>("NodeTypeProvider") as INodeTypeProvider;
                        var parser = queryCompiler
                            .InnerInvoke<QueryCompiler>("CreateQueryParser", nodeTypeProvider) as QueryParser;
                        return parser.GetParsedQuery(@this.Expression);
                    }

                    throw new NotSupportedException("Can not get QueryModel.");
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
