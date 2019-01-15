using Dawnx.Net.Web;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Gcs(string jsonFile)
        {
            if (!AlertUtility.ConfirmUseOnlineService()) return;
            Console.WriteLine("Connect to dawnx service...");

            var resp = Http.PostFor<JSend>($"{Program.SUPPORT_URL}/JsonToCsFile", new Dictionary<string, object>
            {
                ["Namespace"] = ProjectUtility.RootNamespace,
                ["ClassName"] = Path.GetFileNameWithoutExtension(jsonFile),
                ["Json"] = File.ReadAllText(jsonFile),
            });

            if (resp.IsSuccess())
            {
                var outFile = $"{jsonFile}/../{Path.GetFileNameWithoutExtension(jsonFile)}.cs";

                using (var stream = new FileStream(outFile, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                    writer.Write(resp.data as string);

                Console.WriteLine($"{resp.message}");
                Console.WriteLine($"  File Saved: {Path.GetFullPath(outFile)}");
            }
            else AlertUtility.PrintErrorMessage(resp);
        }

    }
}
