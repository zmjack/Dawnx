using Dawnx.AspNetCore;
using Dawnx.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Install(string name)
        {
            var respJson = Web.Post($"{Program.SUPPORT_URL}/Install", new Dictionary<string, object>
            {
                ["name"] = name,
            });

            var resp = JsonConvert.DeserializeObject<SimpleResponse>(respJson);
            if (resp.Success)
            {
                var model = resp.model as JObject;
                var cli_version = model["cli_version"].Value<string>();

                if (new Version(Program.CLI_VERSION) >= new Version(cli_version))
                {
                    int fileCount = model["count"].Value<int>();
                    int fileDownload = 0;
                    int fileSkip = 0;
                    int fileDone() => fileDownload + fileSkip;
                    int fileVerifySuccess = 0;
                    int fileVerifyFailed = 0;

                    int totalProgressFiledLength = fileCount * 2 + 1;
                    int filenameFiledLength = 67 - totalProgressFiledLength;

                    List<string> extractFileList = new List<string>();

                    foreach (var item in model["files"] as JArray)
                    {
                        var url = item["url"].Value<string>();
                        var md5 = item["md5"].Value<string>();
                        var saveas = $@"{Program.DOWNLOAD_DIRECTORY}\{Path.GetFileName(url)}";
                        var extract = item["extract"].Value<bool>();

                        Console.Write(
                            $"{$"{fileDone() + 1}/{fileCount}".PadLeft(totalProgressFiledLength)} " +
                            $"| " + Path.GetFileName(saveas)
                                .For(_ => _.Length <= filenameFiledLength ? _ : _.Substring(0, filenameFiledLength))
                                .PadRight(filenameFiledLength) +
                            $"|");

                        if (!File.Exists(saveas) || !FileUtility.CheckMD5(saveas, md5))
                        {
                            #region Download files
                            using (var file = new FileStream(saveas, FileMode.Create))
                            {
                                var web = new WebAccess();
                                web.DownloadProgress += (sender, _url, received, length) =>
                                {
                                    Console.SetCursorPosition(72, Console.CursorTop);
                                    Console.Write($"{((double)received / length).ToString("0.00%").PadLeft(7)}");
                                };

                                int retry = 0, allowedRetry = 3;
                                retry:
                                try
                                {
                                    web.GetDownload(file, url);

                                    if (extract)
                                        extractFileList.Add(saveas);
                                    fileDownload++;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine();
                                    if (retry < allowedRetry)
                                    {
                                        Console.WriteLine($"  {ex.Message}, retry {++retry}/{allowedRetry}");
                                        goto retry;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  File can not be downloaded from {url}");
                                        if (ConsoleUtility.AskYN("Retry?"))
                                        {
                                            retry = 0;
                                            goto retry;
                                        }
                                        else
                                        {
                                            fileSkip++;
                                            continue;
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Check file md5
                            Console.SetCursorPosition(72, Console.CursorTop);
                            if (FileUtility.CheckMD5(saveas, md5))
                            {
                                fileVerifySuccess++;
                                Console.WriteLine(" Safe  ");
                            }
                            else
                            {
                                fileVerifyFailed++;
                                Console.WriteLine("WARNING");
                            }
                            #endregion
                        }
                        else
                        {
                            if (extract)
                                extractFileList.Add(saveas);
                            fileDownload++;

                            Console.SetCursorPosition(72, Console.CursorTop);
                            Console.WriteLine(" Found ");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine(
                        $"Result: {fileDownload} downloaded, " +
                        $"{fileVerifySuccess} safe, " +
                        $"{fileVerifyFailed} warning. " +
                        $"{fileSkip} skiped.");
                    Console.WriteLine($"---- All files has been downloaded using engine Dawnx.Net.Http.Web ----{Environment.NewLine}");

                    // Setup
                    if (ConsoleUtility.AskYN("Setup now?"))
                    {
                        Console.WriteLine();
                        foreach (var file in extractFileList)
                        {
                            ZipFile.ExtractToDirectory(file, Directory.GetCurrentDirectory(), true);
                            Console.WriteLine($"Extract {file} done.");
                        }
                        Console.WriteLine($"---- Extract files completed ----{Environment.NewLine}");
                    }
                }
                else Console.WriteLine($"Install service requires the lowest cli tool version: {cli_version}.");
            }
            else ConsoleUtility.PrintErrorMessage(resp);
        }

    }
}
