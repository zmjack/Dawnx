using Dawnx.Data;
using Dawnx.Net.Web;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Dawnx.Tools
{
    public class InstallCommand : ICommand
    {
        public void Help()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs args)
        {
            var name = args[1];

            var resp = Http.PostFor<JSend>($"{Program.SUPPORT_URL}/Install", new Dictionary<string, object>
            {
                ["name"] = name,
            });

            try
            {
                if (resp.IsSuccess())
                {
                    var model = resp.data as JObject;
                    var cli_version = model["cli_version"].Value<string>();

                    if (new Version(Program.CLI_VERSION) >= new Version(cli_version))
                    {
                        int fileCount = model["count"].Value<int>();
                        int fileDownload = 0;
                        int fileSkip = 0;
                        int fileDone() => fileDownload + fileSkip;
                        int fileVerifySuccess = 0;
                        int fileVerifyFailed = 0;

                        int colLength1 = fileCount * 2 + 1;
                        int[] tableLengths = new[] { colLength1, 67 - colLength1, 7 };

                        List<string> extractFileList = new List<string>();

                        foreach (var item in model["files"] as JArray)
                        {
                            var url = item["url"].Value<string>();
                            var md5 = item["md5"].Value<string>();
                            var fileName = item["fileName"].Value<string>();
                            var saveas = $@"{Program.DOWNLOAD_DIRECTORY}\{fileName}";
                            var extract = item["extract"].Value<bool>();

                            if (!File.Exists(saveas) || FileUtility.ComputeMD5(saveas) != md5)
                            {
                                #region Download files
                                using (var file = new FileStream(saveas, FileMode.Create))
                                {
                                    var web = new HttpAccess();
                                    web.DownloadProgress += (sender, _url, received, length) =>
                                    {
                                        Con.Row(new[]
                                        {
                                            $"{fileDone() + 1}/{fileCount}", $"| {fileName}", ((double)received / length).ToString("0.00%")
                                        }, tableLengths);
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
                                        Con.Line();
                                        if (retry < allowedRetry)
                                        {
                                            Con.Print($"  {ex.Message}, retry {++retry}/{allowedRetry}").Line();
                                            goto retry;
                                        }
                                        else
                                        {
                                            Con.Print($"  File can not be downloaded from {url}").Line();

                                            Con.AskYN("Retry?", out var ansRetry);
                                            if (ansRetry) { retry = 0; goto retry; }
                                            else { fileSkip++; continue; }
                                        }
                                    }
                                }
                                #endregion

                                #region Check file md5
                                var status = "";
                                if (FileUtility.ComputeMD5(saveas) == md5)
                                {
                                    fileVerifySuccess++;
                                    status = "Safe";
                                }
                                else
                                {
                                    fileVerifyFailed++;
                                    status = "WARNING";
                                }

                                Con.Row(new[]
                                {
                                    $"{fileDone()}/{fileCount}",
                                    $"| {Path.GetFileName(saveas)}",
                                    status
                                }, tableLengths).Line();
                                #endregion
                            }
                            else
                            {
                                if (extract)
                                    extractFileList.Add(saveas);
                                fileDownload++;

                                Con.Row(new[]
                                {
                                    $"{fileDone()}/{fileCount}",
                                    $"| {Path.GetFileName(saveas)}",
                                    "Found"
                                }, tableLengths).Line();
                            }
                        }

                        Con
                            .Line()
                            .Print($"  " +
                                $"{fileDownload} downloaded." +
                                $"  {fileVerifySuccess} safe, {fileVerifyFailed} warning, {fileSkip} skiped.").Line()
                            .Print($"---- All files has been downloaded using engine {typeof(Http).FullName} ----").Line()
                            .Line();

                        // Setup
                        void extractFiles()
                        {
                            foreach (var file in extractFileList)
                            {
                                ZipFile.ExtractToDirectory(file, Program.TargetProjectInfo.ProjectRoot, true);
                                Con.Print($"Extract {file} done.").Line();
                            }
                            Con
                                .Print($"---- Extract files completed ----").Line()
                                .Line();
                        };

                        if (fileVerifyFailed > 0)
                        {
                            Con.AskYN("Setup now?", out var ans);
                            if (ans) extractFiles();
                        }
                        else extractFiles();
                    }
                    else Con.Print($"Install service requires the lowest cli tool version: {cli_version}.").Line();
                }
                else AlertUtility.PrintErrorMessage(resp);

            }
            catch (JsonReaderException ex)
            {
                Con.Print($"Error occurred. ({ex.Message})").Line();
            }
        }

    }

}
