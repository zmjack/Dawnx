﻿using Dawnx.Algorithms.Tree;
using Dawnx.Sequences;
using NStandard;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.Net.Web
{
    public class FtpListTree : Tree<FtpListItem>
    {
        public enum EPlatform { Windows, Linux }
        public EPlatform Platform { get; set; }

        public FtpAccess FtpAccess { get; set; }
        public string RelativePath { get; set; }

        public FtpListTree(FtpAccess ftpAccess, string relativePath)
            : base(new FtpListItem(ftpAccess.BaseUrl))
        {
            FtpAccess = ftpAccess;
            RelativePath = relativePath.For(_ => _.EndsWith("/") ? _ : $"{_}/");
        }

        public FtpListTree Sync(bool recursive = false)
        {
            if (Model.Type == FtpListItem.ItemType.File) return this;

            Clear();
            var listDetail = FtpAccess.ListDirectoryDetails(RelativePath);

            var lines = listDetail.GetPureLines().ToArray();
            if (lines.Any())
            {
                var firstCol = lines[0].For(_ => _.Substring(0, 17));
                Platform = DateTime.TryParseExact(firstCol, "MM-dd-yy  hh:mmtt", new CultureInfo("en-US"), DateTimeStyles.None, out var dt)
                    ? EPlatform.Windows : EPlatform.Linux;
            }

            switch (Platform)
            {
                //TODO: To check the file name part, if the file name contains at least one blank.

                case EPlatform.Windows:
                    // if Windows
                    /* 08-10-11  12:02PM       <DIR>          Version2
                     * 06-25-09  02:41PM            144700153 image34.gif
                     * 06-25-09  02:51PM            144700153 updates.txt
                     * 11-04-10  02:45PM            144700214 digger.tif
                     */
                    AddRange(lines.Select(line =>
                    {
                        var groups = line.Resolve(new Regex(@"^(.+?(?=AM|PM)(?:AM|PM))\s+(<DIR>|\d+)\s+(.+)$"));
                        DateTime lastWriteTime;
                        DateTime.TryParseExact(groups[1][0], "MM-dd-yy  hh:mmtt", new CultureInfo("en-US"), DateTimeStyles.None, out lastWriteTime);
                        var isDirectory = groups[2][0] == "<DIR>";
                        var size = isDirectory ? 0 : long.Parse(groups[2][0]);
                        var name = groups[3][0];

                        var item = new FtpListTree(FtpAccess, isDirectory ? $"{RelativePath}/{name}/" : "");
                        item.Model.Then(_ =>
                        {
                            _.LastWriteTime = lastWriteTime;
                            _.Type = isDirectory ? FtpListItem.ItemType.Directory : FtpListItem.ItemType.File;
                            _.Size = size;
                            _.Name = name;
                        });
                        return item;
                    }).ToArray());
                    break;

                case EPlatform.Linux:
                    // if Linux
                    /* d--x--x--x    2 ftp      ftp          4096 Mar 07  2002 bin
                     * -rw-r--r--    1 ftp      ftp        659450 Jun 15 05:07 TEST.TXT
                     * -rw-r--r--    1 ftp      ftp      101786380 Sep 08  2008 TEST03-05.TXT
                     * drwxrwxr-x    2 ftp      ftp          4096 May 06 12:24 dropoff
                     */
                    AddRange(lines.Select(line =>
                    {
                        var groups = line.Resolve(new Regex(@"^(d?).+?\s+\d+\s+.+?\s+.+?\s+(\d+)\s+(.+)\s+(.+)$"));
                        var lastWriteTime = groups[3][0].For(timePart =>
                        {
                            var creationTimeGroups = timePart.Resolve(new Regex(@"(\w+)\s+(\d+)\s+(.+)"));
                            var month = MonthSequence.GetMonth(creationTimeGroups[1][0]);
                            var day = int.Parse(creationTimeGroups[2][0]);
                            DateTime lastModify;

                            //if like 09:00
                            if (creationTimeGroups[3][0].Contains(":"))
                            {
                                var timeParts = creationTimeGroups[3][0].Split(':');
                                lastModify = new DateTime(DateTime.Now.Year, month, day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), 0);
                            }
                            //if like 2002
                            else lastModify = new DateTime(int.Parse(creationTimeGroups[3][0]), month, day);

                            return lastModify;
                        });
                        var isDirectory = groups[1][0] == "d";
                        var size = long.Parse(groups[2][0]);
                        var name = groups[4][0];

                        var item = new FtpListTree(FtpAccess, isDirectory ? $"{RelativePath}/{name}/" : "");
                        item.Model.Then(_ =>
                        {
                            _.LastWriteTime = lastWriteTime;
                            _.Type = isDirectory ? FtpListItem.ItemType.Directory : FtpListItem.ItemType.File;
                            _.Size = size;
                            _.Name = name;
                        });
                        return item;
                    }).ToArray());
                    break;

                default: throw new NotSupportedException();
            }

            if (recursive)
                Children.Each(x => (x as FtpListTree).Sync(recursive));

            return this;
        }

        public override string Key { get => Model.Name; set => Model.Name = value; }

    }
}
