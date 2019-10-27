using Microsoft.EntityFrameworkCore;

namespace NLinq
{
    public interface IEntityTracker
    {
        void OnInserting(DbContext context);
        void OnUpdating(DbContext context);
        void OnDeleting(DbContext context);
    }

}
