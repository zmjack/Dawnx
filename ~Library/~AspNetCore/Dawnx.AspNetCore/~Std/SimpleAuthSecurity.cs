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
            => _Rsa.Encrypt(source.GetBytes(Encoding.UTF8)).Base64Encode();
        public string Decrypt(string encrypted)
            => _Rsa.Decrypt(encrypted.Base64Decode()).GetString(Encoding.UTF8);

        public string SignData(string data) =>
            _Rsa.SignData(data.GetBytes(Encoding.UTF8)).Base64Encode();
        public bool VerifyData(string data, string signature) =>
            _Rsa.VerifyData(data.GetBytes(Encoding.UTF8), signature.Base64Decode());

    }
}
