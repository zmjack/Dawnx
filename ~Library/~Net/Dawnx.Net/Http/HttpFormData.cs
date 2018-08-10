using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dawnx.Net.Http
{
    internal class HttpFormData
    {
        public readonly byte[] CRLF = new byte[] { 13, 10 };

        public List<UploadFileData> Files = new List<UploadFileData>();
        public List<UploadData> Values = new List<UploadData>();
        public Encoding Encoding { get; private set; }
        private string _boundary;

        public string ContentType => $"multipart/form-data; boundary={_boundary}";

        public HttpFormData(Encoding encoding)
        {
            _boundary = Guid.NewGuid().ToString().Replace("-", "");
            Encoding = encoding;
        }

        private byte[] GetPartHeader(string name, string fileName)
        {
            return Encoding.GetBytes($@"--{_boundary}
Content-Disposition: form-data; name=""{name}""; filename=""{fileName}""
Content-Type: application/octet-stream" + "\r\n\r\n");
        }
        private byte[] GetPartHeader(string name)
        {
            return Encoding.GetBytes($@"--{_boundary}
Content-Disposition: form-data; name=""{name}""" + "\r\n\r\n");
        }

        public void AddFile(string name, string fileName, string path)
        {
            Files.Add(new UploadFileData
            {
                Name = name,
                FileName = fileName,
                Data = File.ReadAllBytes(path).Concat(CRLF).ToArray(),
            });
        }

        public void AddFile(string name, string fileName, byte[] data)
        {
            Files.Add(new UploadFileData
            {
                Name = name,
                FileName = fileName,
                Data = data.Concat(CRLF).ToArray(),
            });
        }

        public void AddData(string name, byte[] data)
        {
            Values.Add(new UploadData { Key = name, Value = data.Concat(CRLF).ToArray() });
        }

        public static implicit operator byte[] (HttpFormData @this)
        {
            var content = new byte[0];
            foreach (var value in @this.Values)
            {
                content = content
                    .Concat(@this.GetPartHeader(value.Key))
                    .Concat(value.Value).ToArray();
            }
            foreach (var data in @this.Files)
            {
                content = content
                    .Concat(@this.GetPartHeader(data.Name, data.FileName))
                    .Concat(data.Data).ToArray();
            }
            content = content.Concat(Encoding.UTF8.GetBytes($"--{@this._boundary}--")).ToArray();

            return content;
        }
    }
}
