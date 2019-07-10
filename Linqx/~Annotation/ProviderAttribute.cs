using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Linqx
{
    public abstract class IProvider<TModel, TProvider>
    {
        public abstract TProvider ConvertToProvider(TModel model);
        public abstract TModel ConvertFromProvider(TProvider provider);
    }

    public class ProviderAttribute : Attribute
    {
        public Type ProviderType { get; set; }

        public ProviderAttribute(Type providerType)
        {
            ProviderType = providerType;
        }
    }

}
