namespace Dawnx.Compress
{
    public partial class ZipStream
    {
        public class UpdateScope : Scope<ZipStream, UpdateScope>
        {
            public UpdateScope(ZipStream model) : base(model)
            {
                model.ZipFile.BeginUpdate();
            }

            public override void Disposing()
            {
                Model.ZipFile.CommitUpdate();
            }
        }
    }
}
