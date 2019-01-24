using Dawnx.AspNetCore.AppSupport.Entities;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dawnx.AspNetCore.AppSupport
{
    public class AppRegistryManager<TDbContext, TAppRegistryItem>
        where TDbContext : DbContext, IAppRegistryDbContext
        where TAppRegistryItem : class, IAppRegistryItem, new()
    {
        public TDbContext Context { get; private set; }

        public AppRegistryManager()
        {
            Context = DIUtility.GetEntryService<TDbContext>();
        }

        /// <summary>
        /// Begin IsolationLevel.Serializable level transaction.
        /// </summary>
        /// <returns></returns>
        public AppRegistryTransaction<TDbContext, TAppRegistryItem> BeginAutoTransaction()
            => new AppRegistryTransaction<TDbContext, TAppRegistryItem>(this);

        public TAppRegistryItem GetGlobalItem()
        {
            var result = Context.AppRegistries.Where(x => x.Scope == AppRegistryScope.Global);

            var registryItem = new TAppRegistryItem();
            var props = registryItem.GetType().GetProperties();
            foreach (var item in result)
            {
                var prop = props.FirstOrDefault(x => x.Name == item.Key);
                if (!(prop is null))
                    prop.SetValue(registryItem, Convert.ChangeType(item.Value, prop.PropertyType));
            }

            return registryItem.Proxy(new RegistryItemProxy<TDbContext, TAppRegistryItem>(this));
        }

        public void SetGlobalItemValue(string key, object value)
        {
            if (Scope<IDbContextTransaction, AppRegistryTransaction<TDbContext, TAppRegistryItem>>.Current is null)
                throw new MemberAccessException("This method require an transation.");

            var item = Context.AppRegistries
                .Where(x => x.Scope == AppRegistryScope.Global)
                .FirstOrDefault(x => x.Key == key);

            if (item is null)
            {
                Context.AppRegistries.Add(new AppRegistry
                {
                    Scope = AppRegistryScope.Global,
                    Key = key,
                    Value = value.ToString(),
                });
            }
            else item.Value = value.ToString();
        }

        public TAppRegistryItem GetPersonalItem(string group)
        {
            var result = Context.AppRegistries
                .Where(x => x.Scope == AppRegistryScope.Personal)
                .Where(x => x.Group == group);

            var registryItem = new TAppRegistryItem();
            var props = registryItem.GetType().GetProperties();
            foreach (var item in result)
            {
                var prop = props.FirstOrDefault(x => x.Name == item.Key);
                if (!(prop is null))
                    prop.SetValue(registryItem, Convert.ChangeType(item.Value, prop.PropertyType));
            }

            return registryItem.Proxy(new RegistryItemProxy<TDbContext, TAppRegistryItem>(this));
        }

        public void SetPersionItemValue(string group, string key, object value)
        {
            if (Scope<IDbContextTransaction, AppRegistryTransaction<TDbContext, TAppRegistryItem>>.Current is null)
                throw new MemberAccessException("This method require an transation.");

            var item = Context.AppRegistries
                .Where(x => x.Scope == AppRegistryScope.Personal)
                .FirstOrDefault(x => x.Group == group && x.Key == key);

            if (item is null)
            {
                Context.AppRegistries.Add(new AppRegistry
                {
                    Scope = AppRegistryScope.Global,
                    Key = key,
                    Value = value.ToString(),
                });
            }
            else item.Value = value.ToString();
        }

        public void SaveChanges() => Context.SaveChanges();

    }
}
