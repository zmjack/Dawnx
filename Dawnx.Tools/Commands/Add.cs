using Dawnx.AspNetCore;
using Dawnx.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Add(string name)
        {
            if (!ConsoleUtility.ConfirmUseOnlineService()) return;
            Console.WriteLine("Connect to dawnx service...");

            var respJson = Web.Post($"{Program.SUPPORT_URL}/Add", new Dictionary<string, object>
            {
                ["Namespace"] = ProjectUtility.RootNamespace,
                ["Name"] = name,
            });

            var resp = JsonConvert.DeserializeObject<SimpleResponse>(respJson);
            if (resp.Success)
            {
                //TODO: New feture
                throw new NotSupportedException();
                //var outFile = $"{jsonFile}/../{Path.GetFileNameWithoutExtension(jsonFile)}.cs";

                //using (var stream = new FileStream(outFile, FileMode.Create))
                //using (var writer = new StreamWriter(stream))
                //    writer.Write(resp.model as string);

                //Console.WriteLine($"{resp.message}");
                //Console.WriteLine($"  File Saved: {Path.GetFullPath(outFile)}");
            }
            else ConsoleUtility.PrintErrorMessage(resp);
        }

    }
}
