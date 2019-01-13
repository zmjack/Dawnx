using Dawnx.AspNetCore.LiveRegistry.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dawnx.AspNetCore.LiveRegistry
{
    public class AppRegistryManager<TDbContext, TAppRegistryItem>
        where TDbContext : DbContext, IAppRegistryDbContext
        where TAppRegistryItem : class, new()
    {
        private readonly TDbContext _context;

        public AppRegistryManager()
        {
            _context = DIUtility.GetEntryService<TDbContext>();
        }

        public TAppRegistryItem GetGlobalItem()
        {
            var result = _context.AppRegistries.Where(x => x.Scope == AppRegistryScope.Global);

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

        public void SetGlobalItemValue(string key, string value)
        {
            using (var scope = _context.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var item = _context.AppRegistries
                    .Where(x => x.Scope == AppRegistryScope.Global)
                    .FirstOrDefault(x => x.Key == key);
                item.Value = value;

                _context.SaveChanges();
                scope.Commit();
            }
        }

        public TAppRegistryItem GetPersonalItem()
        {
            //TODO: to support.
            throw new NotSupportedException();
        }

    }
}
