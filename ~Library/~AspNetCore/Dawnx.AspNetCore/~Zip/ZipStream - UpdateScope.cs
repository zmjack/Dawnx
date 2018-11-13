using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dawnx.AspNetCore
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
