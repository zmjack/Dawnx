using Def;
using NStandard;
using System;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public class FtpAccess
    {
        public const int RECOMMENDED_BUFFER_SIZE = 256 * 1024;      // 256 KB

        public delegate void ProgressHandler(FtpAccess sender, long done, long length);
        public event ProgressHandler DownloadProgress;
        public event ProgressHandler UploadProgress;

        public Uri BaseUrl { get; private set; }
        public FtpStateContainer StateContainer { get; private set; }

        public FtpAccess(string baseUrl) : this(baseUrl, new FtpStateContainer()) { }
        public FtpAccess(string baseUrl, FtpStateContainer config)
        {
            BaseUrl = new Uri(baseUrl);

            if (config != null)
                StateContainer = config;
            else StateContainer = new FtpStateContainer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="method">(Be recommended to use WebRequestMethods or FtpVerb.)</param>
        /// <returns></returns>
        protected FtpWebRequest NewRequest(string relativePath, string method)
        {
            var request = ((FtpWebRequest)WebRequest.Create(new Uri(BaseUrl.AbsoluteUri + relativePath))).Then(_ =>
            {
                _.Method = method;

                _.UseBinary = StateContainer.UseBinary;
                _.UsePassive = StateContainer.UsePassive;
                _.Timeout = -1;
                _.Credentials = new NetworkCredential(StateContainer.UserName, StateContainer.Password);

                if (StateContainer.UseProxy)
                {
                    if (!string.IsNullOrEmpty(StateContainer.ProxyAddress))
                    {
                        _.Proxy = new WebProxy
                        {
                            Address = new Uri(StateContainer.ProxyAddress),
                            Credentials = new NetworkCredential
                            {
                                UserName = StateContainer.ProxyUsername,
                                Password = StateContainer.ProxyPassword,
                            }
                        };
                    }
                }
            });
            return request;
        }

        public void DownloadFile(Stream receiver, string relativePath, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            //var request = NewRequest(WebRequestMethods.Ftp.DownloadFile);
            var request = NewRequest(relativePath, FtpVerb.RETR);

            using (var response = request.GetResponse())
            {
                long received = 0;
                using (var stream = response.GetResponseStream())
                {
                    stream.Reading(bufferSize, (buffer, readLength) =>
                    {
                        receiver.Write(buffer, 0, readLength);
                        received += readLength;
                        DownloadProgress?.Invoke(this, received, response.ContentLength);
                    });
                }
            }
        }

        public FtpListTree ListTree(string relativePath = "", bool recursive = false)
        {
            return new FtpListTree(this, relativePath)
                .Then(_ => _.Model.Type = FtpListItem.ItemType.Directory)
                .Sync(recursive);
        }

        public string ListDirectoryDetails(string relativePath, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            //var request = NewRequest(WebRequestMethods.Ftp.ListDirectoryDetails);
            var request = NewRequest(relativePath, FtpVerb.LIST);

            var memory = new MemoryStream();
            using (var response = request.GetResponse())
            {
                long received = 0;
                using (var stream = response.GetResponseStream())
                {
                    stream.Reading(bufferSize, (buffer, readLength) =>
                    {
                        memory.Write(buffer, 0, readLength);
                        received += readLength;
                        DownloadProgress?.Invoke(this, received, response.ContentLength);
                    });
                }
            }

            return memory.ToArray().String(StateContainer.Encoding);
        }

        public void UploadFile(Stream bodyStream, string relativePath, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            //var request = NewRequest(WebRequestMethods.Ftp.UploadFile);
            var request = NewRequest(relativePath, FtpVerb.STOR);

            request.ContentLength = bodyStream.Length;
            using (var stream = request.GetRequestStream())
            {
                bodyStream.Writing(stream, bufferSize, (writeTarget, buffer, totalWrittenLength) =>
                {
                    UploadProgress?.Invoke(this, totalWrittenLength, bodyStream.Length);
                });
            }
        }

        public void MakeDirectory(string relativePath)
        {
            //var request = NewRequest(WebRequestMethods.Ftp.MakeDirectory);
            var request = NewRequest(relativePath, FtpVerb.MKD);

            using (var response = request.GetResponse())
            {
            }
        }

    }

}

