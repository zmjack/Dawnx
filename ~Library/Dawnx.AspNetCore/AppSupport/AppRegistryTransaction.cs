using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Dawnx.AspNetCore.AppSupport
{
    public class AppRegistryTransaction<TDbContext, TAppRegistryItem> : Scope<IDbContextTransaction, AppRegistryTransaction<TDbContext, TAppRegistryItem>>
        where TDbContext : DbContext, IAppRegistryDbContext
        where TAppRegistryItem : class, IAppRegistryItem, new()
    {
        public AppRegistryTransaction(AppRegistryManager<TDbContext, TAppRegistryItem> manager)
            : this(manager.Context.Database.BeginTransaction(IsolationLevel.Serializable))
        {
        }
        private AppRegistryTransaction(IDbContextTransaction model) : base(model) { }

        public override void Disposing()
        {
            Model.Commit();
            Model.Dispose();
        }

    }
}
