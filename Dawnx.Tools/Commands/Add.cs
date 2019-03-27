using Dawnx.Net.Web;
using System;
using System.Collections.Generic;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Add(string name)
        {
            if (!AlertUtility.ConfirmUseOnlineService()) return;
            Con.PrintLine("Connect to dawnx service...");

            var resp = Http.PostFor<JSend>($"{Program.SUPPORT_URL}/Add", new Dictionary<string, object>
            {
                ["Namespace"] = ProjectUtility.RootNamespace,
                ["Name"] = name,
            });

            if (resp.IsSuccess())
            {
                //TODO: New feture
                throw new NotSupportedException();
                //var outFile = $"{jsonFile}/../{Path.GetFileNameWithoutExtension(jsonFile)}.cs";

                //using (var stream = new FileStream(outFile, FileMode.Create))
                //using (var writer = new StreamWriter(stream))
                //    writer.Write(resp.model as string);

                //Con.Line($"{resp.message}");
                //Con.Line($"  File Saved: {Path.GetFullPath(outFile)}");
            }
            else AlertUtility.PrintErrorMessage(resp);
        }

    }
}
