﻿using Dawnx.Compress;
using Dawnx.Data;
using Dawnx.Security.AesSecurity;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.Tools
{
    public class CompressCommand : ICommand
    {
        private object archive;
        private object sources;

        public void Help()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs args)
        {
            string[] jsonFiles = args.Contents.Skip(1).ToArray();

            if (!jsonFiles.Any())
            {
                var json = "compress.json";

                if (!File.Exists(json))
                {
                    using (var file = new FileStream(json, FileMode.Create))
                    using (var stream = new StreamWriter(file))
                    {
                        stream.WriteLine($@"[
  {{
    ""{nameof(archive)}"": ""<Destination>.zip"",
    ""{nameof(sources)}"": {{
      ""<Archive-Directory or Archive-File>"": [""<Real-File>""]
    }}
  }}
]");
                    }

                    Console.Error.WriteLine("File `compress.json` is created.");
                    return;
                }
                else jsonFiles = new string[] { json };
            }

            foreach (var jsonFile in jsonFiles)
            {
                var json = File.ReadAllText(jsonFile);
                var zipDefs = JsonConvert.DeserializeObject(json) as JArray;

                foreach (var zipDef in zipDefs.Select(x => x.Value<JObject>()))
                {
                    string archive;
                    var sources = new Dictionary<string, string>();

                    if (!zipDef.Properties().Any(x => x.Name == nameof(archive)))
                        throw new ArgumentException($"The compress json file has no property(`{nameof(archive)}`).");
                    if (zipDef[nameof(archive)].Type != JTokenType.String
                        || !FileUtility.IsFilePath(zipDef[nameof(archive)].Value<string>()))
                        throw new ArgumentException($"The compress json file's property(`{nameof(archive)}`) must be a file path.");

                    if (!zipDef.Properties().Any(x => x.Name == nameof(sources)))
                        throw new ArgumentException($"The compress json file has no property(`${nameof(sources)}`).");
                    if (zipDef["sources"].Type != JTokenType.Object)
                        throw new ArgumentException($"The compress json file's property(`${nameof(sources)}`) must be a {JTokenType.Object}.");

                    archive = zipDef[nameof(archive)].Value<string>();
                    using (var zip = new ZipStream())
                    {
                        foreach (var source in zipDef["sources"].Value<JObject>().Properties())
                        {
                            var ext = Path.GetExtension(source.Name);

                            // Guess the name is a directory name
                            if (ext == string.Empty)
                            {
                                var dir = source.Name;
                                if (DirectoryUtility.IsDirectoryPath(dir))
                                {
                                    switch (source.Value.Type)
                                    {
                                        case JTokenType.String:
                                            {
                                                var file = source.Value.Value<string>();
                                                if (File.Exists(file))
                                                    zip.AddFileEntry(Path.Combine(source.Name, Path.GetFileName(file)), file);
                                                else throw new FileNotFoundException();
                                            }
                                            break;

                                        case JTokenType.Array:
                                            foreach (var file in source.Value.Value<JArray>().Select(x => x.Value<string>()))
                                            {
                                                if (File.Exists(file))
                                                    zip.AddFileEntry(Path.Combine(source.Name, Path.GetFileName(file)), file);
                                                else throw new FileNotFoundException();
                                            }
                                            break;

                                        default:
                                            throw new ArgumentException($"The compress json file's property(`${nameof(sources)}/${source.Name}`)'s value must be a file path or file path list.");
                                    }

                                }
                                else throw new ArgumentException($"The compress json file's property(`${nameof(sources)}/${source.Name}`) must be a directory path or file path.");
                            }
                            // Guess the name is a file name
                            else
                            {
                                if (FileUtility.IsFilePath(source.Name))
                                {
                                    if (source.Value.Type == JTokenType.String)
                                    {
                                        var file = source.Value.Value<string>();
                                        if (File.Exists(file))
                                            zip.AddFileEntry(source.Name, file);
                                        else throw new FileNotFoundException();
                                    }
                                    else throw new ArgumentException($"The compress json file's property(`${nameof(sources)}/${source.Name}`)'s value must be a file path.");
                                }
                                else throw new ArgumentException($"The compress json file's property(`${nameof(sources)}/${source.Name}`) must be a directory path or file path.");
                            }
                        }

                        zip.SaveAs(archive);
                    }

                }
            }

        }

    }
}

