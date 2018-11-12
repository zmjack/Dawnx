using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Dawnx.AspNetCore
{
    public partial class ZipStream
    {
        private Stream MappedStream;
        private ZipFile StoredZipFile;

        /// <summary>
        /// Creates a new <see cref="ZipStream"/> whose data will be stored on a stream.
        /// </summary>
        public ZipStream()
        {
            MappedStream = new MemoryStream();
            StoredZipFile = ZipFile.Create(MappedStream);
            StoredZipFile.BeginUpdate();
        }

        /// <summary>
        /// Opens a Zip file with the given name for reading.
        /// </summary>
        /// <param name="path"></param>
        public ZipStream(string path)
        {
            MappedStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            StoredZipFile = new ZipFile(MappedStream);
        }

        /// <summary>
        /// Opens a Zip file reading the given System.IO.Stream.
        /// </summary>
        /// <param name="stream"></param>
        public ZipStream(Stream stream)
        {
            MappedStream = stream;
            StoredZipFile = new ZipFile(stream);
        }

        /// <summary>
        /// Add a file entry with data.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public ZipStream AddFile(string path, string entryName)
        {
            StoredZipFile.Add(path, entryName);
            return this;
        }

        /// <summary>
        /// Add a file entry with data.
        /// </summary>
        /// <param name="inStream"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public ZipStream AddFile(Stream inStream, string entryName)
        {
            StoredZipFile.Add(new StaticDataSource(inStream), entryName);
            return this;
        }

        /// <summary>
        /// Add a file entry with data.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public ZipStream AddData(byte[] bytes, string entryName)
        {
            StoredZipFile.Add(new StaticDataSource(new MemoryStream(bytes)), entryName);
            return this;
        }

        /// <summary>
        /// Add a file entry with data.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public ZipStream AddData(string text, string entryName)
        {
            StoredZipFile.Add(new StaticDataSource(new MemoryStream(text.Bytes())), entryName);
            return this;
        }

        /// <summary>
        /// Set the password to be used for encrypting/decrypting files.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public ZipStream SetPassword(string password)
        {
            StoredZipFile.Password = password;
            return this;
        }

        /// <summary>
        /// Set the file comment to be recorded.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ZipStream SetComment(string comment)
        {
            StoredZipFile.SetComment(comment);
            return this;
        }

        /// <summary>
        /// Save the Zip file.
        /// </summary>
        public void Save()
        {
            StoredZipFile.CommitUpdate();
        }

        /// <summary>
        /// Save the Zip file as a new file.
        /// </summary>
        /// <param name="path"></param>
        public void SaveAs(string path)
        {
            var position = Position;
            Save();

            Seek(0, SeekOrigin.Begin);
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                this.WriteTo(file, 1024 * 1024);
            Seek(position, SeekOrigin.Begin);
        }

    }
}
