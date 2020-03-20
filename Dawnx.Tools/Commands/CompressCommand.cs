using Dawnx.Compress;
using Dawnx.Utilities;
using DotNetCli;
using NEcho;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NStandard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dawnx.Tools
{
    [Command("Compress", "cp", Description = "Compress files which are listed in config file(default: 'compress.json').")]
    public class CompressCommand : ICommand
    {
        private object archive;
        private object sources;

        public void PrintUsage()
        {
            Console.WriteLine($@"
Usage: dotnet nx (cp|compress) [ConfigFile(.json)=compress.json] ...

ConfigFile:
    {"<value>",20}{"\t"}The path of configuration file. The default file is 'compress.json' (If it doesn't exist, create it).
");
        }

        public void Run(ConArgs cargs)
        {
            if (cargs.Properties.For(x => x.ContainsKey("-h") || x.ContainsKey("--help")))
            {
                PrintUsage();
                return;
            }

            string[] jsonFiles = cargs.Contents.Skip(1).ToArray();

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

                    Console.Error.WriteLine("File compress.json is created.");
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
                        throw new ArgumentException($"The compress json file has no property({nameof(archive)}).");
                    if (zipDef[nameof(archive)].Type != JTokenType.String
                        || !FileUtility.IsFilePath(zipDef[nameof(archive)].Value<string>()))
                        throw new ArgumentException($"The compress json file's property({nameof(archive)}) must be a file path.");

                    if (!zipDef.Properties().Any(x => x.Name == nameof(sources)))
                        throw new ArgumentException($"The compress json file has no property(${nameof(sources)}).");
                    if (zipDef["sources"].Type != JTokenType.Object)
                        throw new ArgumentException($"The compress json file's property(${nameof(sources)}) must be a {JTokenType.Object}.");

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
                                                    zip.AddFileEntry(Path.Combine(dir, Path.GetFileName(file)), file);
                                                else throw new FileNotFoundException($"Can not find the file({file}).");
                                            }
                                            break;

                                        case JTokenType.Array:
                                            var files = source.Value.Value<JArray>().Select(x => x.Value<string>());
                                            zip.AddDictionary(dir);
                                            foreach (var file in files)
                                            {
                                                if (File.Exists(file))
                                                    zip.AddFileEntry(Path.Combine(dir, Path.GetFileName(file)), file);
                                                else throw new FileNotFoundException($"Can not find the file({file}).");
                                            }
                                            break;

                                        default:
                                            throw new ArgumentException($"The compress json file's property(${nameof(sources)}/${dir})'s value must be a file path or file path list.");
                                    }

                                }
                                else throw new ArgumentException($"The compress json file's property(${nameof(sources)}/${dir}) must be a directory path or file path.");
                            }
                            // Guess the name is a file name
                            else
                            {
                                var embededFile = source.Name;

                                if (FileUtility.IsFilePath(embededFile))
                                {
                                    if (source.Value.Type == JTokenType.String)
                                    {
                                        var file = source.Value.Value<string>();
                                        if (File.Exists(file))
                                            zip.AddFileEntry(embededFile, file);
                                        else throw new FileNotFoundException($"Can not find the file({file}).");
                                    }
                                    else throw new ArgumentException($"The compress json file's property(${nameof(sources)}/${embededFile})'s value must be a file path.");
                                }
                                else throw new ArgumentException($"The compress json file's property(${nameof(sources)}/${embededFile}) must be a directory path or file path.");
                            }
                        }

                        zip.SaveAs(archive);
                        Console.WriteLine($"Zip file has been created: {new FileInfo(archive).FullName}");
                    }

                }
            }

        }

        public void Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}

