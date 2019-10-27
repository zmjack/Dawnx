using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NLinq
{
    public interface IEntityTracker
    {
        void OnInserting(DbContext context);
        void OnUpdating(DbContext context, PropertyValues origin);
        void OnDeleting(DbContext context);
    }

}
