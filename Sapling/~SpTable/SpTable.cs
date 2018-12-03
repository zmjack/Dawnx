using Dawnx;
using Dawnx.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sapling
{
    public class SpTable
    {
        public class Config : ISaplingConfig
        {
        }

        public static SpTableSource<TModel> NewSource<TModel>(IEnumerable<TModel> models)
            => new SpTableSource<TModel>(models);
    }

}
