using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.Collections.Generic;

namespace Dawnx.Compress
{
    public partial class ZipStream : IEnumerable<ZipEntry>
    {
        public IEnumerator<ZipEntry> GetEnumerator()
        {
            IEnumerable<ZipEntry> Iterator()
            {
                foreach (var entry in ZipFile)
                    yield return entry as ZipEntry;
            }
            return Iterator().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => ZipFile.GetEnumerator();
    }
}
