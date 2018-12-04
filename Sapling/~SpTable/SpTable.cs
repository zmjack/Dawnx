using System.Collections.Generic;

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
