using Dawnx.Security;
using System.Security.Cryptography;
using System.Text;

namespace Dawnx.AspNetCore
{
    public class SimpleAuthSecurity
    {
        private RSA _Rsa = RSA.Create();

        public void Init(string xmlString) => _Rsa.FromXmlStringStd(xmlString);

        public string Encrypt(string source)
            => _Rsa.Encrypt(source.Bytes(Encoding.UTF8)).Base64String();
        public string Decrypt(string encrypted)
            => _Rsa.Decrypt(encrypted.BytesFromBase64()).String(Encoding.UTF8);

        public string SignData(string data) =>
            _Rsa.SignData(data.Bytes(Encoding.UTF8)).Base64String();
        public bool VerifyData(string data, string signature) =>
            _Rsa.VerifyData(data.Bytes(Encoding.UTF8), signature.BytesFromBase64());

    }
}
