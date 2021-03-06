﻿using Def;
using NStandard;
using NStandard.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dawnx.Net.Web
{
    internal class HttpFormData
    {
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

        public void AddFile(string name, string fileName, Stream stream)
        {
            Files.Add(new UploadFileData
            {
                Name = name,
                FileName = fileName,
                Stream = stream,
            });
        }

        public void AddData(string name, byte[] data)
        {
            Values.Add(new UploadData { Key = name, Stream = new MemoryStream(data) });
        }

        public Stream GetStream()
        {
            //TODO: To simply this method
            return new SequenceInputStream(EnumerableEx.Concat(new[]
            {
                Values.Select(x => new SequenceInputStream(new[]
                {
                    new MemoryStream(GetPartHeader(x.Key)),
                    x.Stream,
                    new MemoryStream(ControlChars.CrLfBytes),
                }) as Stream),
                Files.Select(x => new SequenceInputStream(new[]
                {
                    new MemoryStream(GetPartHeader(x.Name, x.FileName)),
                    x.Stream,
                    new MemoryStream(ControlChars.CrLfBytes),
                }) as Stream),
                new []
                {
                    new MemoryStream(Encoding.GetBytes($"--{_boundary}--")) as Stream,
                },
            }).ToArray());
        }

    }
}
