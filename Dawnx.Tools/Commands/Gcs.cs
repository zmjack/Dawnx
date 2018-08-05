using Dawnx.AspNetCore;
using Dawnx.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Gcs(string jsonFile)
        {
            Console.WriteLine("This is a online sevice, you will upload your file to dawnx.net.");
            Console.WriteLine("  BUT, we will not collect the documents you provided.");
            Console.WriteLine();

            if (!ConsoleUtility.AskYN("Do you want use this service, and upload your file?")) return;

            Console.WriteLine("Connect to dawnx service...");

            var respJson = Web.Post($"{Program.SUPPORT_URL}/JsonToCsFile", new Dictionary<string, object>
            {
                ["Namespace"] = ProjectUtility.RootNamespace,
                ["ClassName"] = Path.GetFileNameWithoutExtension(jsonFile),
                ["Json"] = File.ReadAllText(jsonFile),
            });

            var resp = JsonConvert.DeserializeObject<SimpleResponse>(respJson);
            if (resp.state == "success")
            {
                var outFile = $"{jsonFile}/../{Path.GetFileNameWithoutExtension(jsonFile)}.cs";

                using (var stream = new FileStream(outFile, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                    writer.Write(resp.model as string);

                Console.WriteLine($"{resp.message}");
                Console.WriteLine($"  File Saved: {Path.GetFullPath(outFile)}");
            }
            else ConsoleUtility.PrintErrorMessage(resp);
        }

    }
}
