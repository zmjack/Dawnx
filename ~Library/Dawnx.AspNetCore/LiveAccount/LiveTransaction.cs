using Microsoft.EntityFrameworkCore.Storage;
using NStandard;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveTransaction : Scope<LiveTransaction>
    {
        public readonly IDbContextTransaction Transaction;

        public LiveTransaction(ILiveManager manager)
        {
            Transaction = manager.Context.Database.BeginTransaction();
        }
        private LiveTransaction(IDbContextTransaction model)
        {
            Transaction = model;
        }

        public override void Disposing()
        {
            Transaction.Commit();
            Transaction.Dispose();
        }

    }
}
