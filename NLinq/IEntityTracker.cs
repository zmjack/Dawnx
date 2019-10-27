using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NLinq
{
    public interface IEntityTracker
    {
        void OnInserting(DbContext context);
        void OnUpdating(DbContext context, PropertyValues original);
        void OnDeleting(DbContext context);
    }

}
