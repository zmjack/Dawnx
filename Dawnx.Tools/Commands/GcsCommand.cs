using Dawnx.Data;
using Dawnx.Net.Web;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Tools
{
    public class GcsCommand : ICommand
    {
        public void PrintUsage()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs args)
        {
            var jsonFile = args[1];

            if (!AlertUtility.ConfirmUseOnlineService()) return;
            Con.Print("Connect to dawnx service...").Line();

            var resp = Http.PostFor<JSend>($"{Program.SUPPORT_URL}/JsonToCsFile", new Dictionary<string, object>
            {
                ["Namespace"] = Program.TargetProjectInfo.RootNamespace,
                ["ClassName"] = Path.GetFileNameWithoutExtension(jsonFile),
                ["Json"] = File.ReadAllText(jsonFile),
            });

            if (resp.IsSuccess())
            {
                var path = $"{Path.GetPathRoot(Path.GetFullPath(jsonFile))}/{Path.GetFileNameWithoutExtension(jsonFile)}.cs";
                File.WriteAllText(path, resp.data as string);

                Con.Print($"{resp.message}").Line();
                Con.Print($"  File Saved: {Path.GetFullPath(path)}").Line();
            }
            else AlertUtility.PrintErrorMessage(resp);
        }

    }

}
