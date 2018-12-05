using Dawnx;
using System.Collections.Generic;

namespace Sapling
{
    public class SpTable
    {
        public static SpTableSource<TModel> NewSource<TModel>(PagedEnumerable<TModel> models)
            => new SpTableSource<TModel>(models);
        public static SpTableSource<TModel> NewSource<TModel>(IEnumerable<TModel> models)
            => new SpTableSource<TModel>(models);
        public static SpTableSource<TModel> NewSource<TModel>(IEnumerable<TModel> models, int page, int pageSize, int pageCount)
            => new SpTableSource<TModel>(models, page, pageSize, pageCount);
    }

}
