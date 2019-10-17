using System;
using System.Collections.Generic;
using System.Text;

namespace Def
{
    public static class FtpVerb
    {
        /// <summary>
        /// AppendFile
        /// </summary>
        public const string APPE = "APPE";

        /// <summary>
        /// DeleteFile
        /// </summary>
        public const string DELE = "DELE";

        /// <summary>
        /// DownloadFile
        /// </summary>
        public const string RETR = "RETR";

        /// <summary>
        /// GetDateTimestamp
        /// </summary>
        public const string MDTM = "MDTM";

        /// <summary>
        /// GetFileSize
        /// </summary>
        public const string SIZE = "SIZE";

        /// <summary>
        /// ListDirectory
        /// </summary>
        public const string NLST = "NLST";

        /// <summary>
        /// ListDirectoryDetails
        /// </summary>
        public const string LIST = "LIST";

        /// <summary>
        /// MakeDirectory
        /// </summary>
        public const string MKD = "MKD";

        /// <summary>
        /// PrintWorkingDirectory
        /// </summary>
        public const string PWD = "PWD";

        /// <summary>
        /// RemoveDirectory
        /// </summary>
        public const string RMD = "RMD";

        /// <summary>
        /// Rename
        /// </summary>
        public const string RENAME = "RENAME";

        /// <summary>
        /// UploadFile
        /// </summary>
        public const string STOR = "STOR";

        /// <summary>
        /// UploadFileWithUniqueName
        /// </summary>
        public const string STOU = "STOU";

    }
}
