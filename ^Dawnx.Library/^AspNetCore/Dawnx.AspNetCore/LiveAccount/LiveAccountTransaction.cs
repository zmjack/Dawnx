using Microsoft.EntityFrameworkCore.Storage;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveAccountTransaction : Scope<IDbContextTransaction, LiveAccountTransaction>
    {
        public LiveAccountTransaction(ILiveAccountManager manager)
            : this(manager.Context.Database.BeginTransaction())
        {
        }
        private LiveAccountTransaction(IDbContextTransaction model) : base(model) { }

        public override void Disposing()
        {
            Model.Commit();
            Model.Dispose();
        }

    }
}
