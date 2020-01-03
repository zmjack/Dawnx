using Microsoft.EntityFrameworkCore.Storage;
using NStandard;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveTransaction : Scope<IDbContextTransaction, LiveTransaction>
    {
        public LiveTransaction(ILiveManager manager)
            : this(manager.Context.Database.BeginTransaction())
        {
        }
        private LiveTransaction(IDbContextTransaction model) : base(model) { }

        public override void Disposing()
        {
            Model.Commit();
            Model.Dispose();
        }

    }
}
